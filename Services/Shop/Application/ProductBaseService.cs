using Application.Entities;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Application.Specifications;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.HelperTypes;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using Shop.Core.Dtos.Product;
using System.Net;

namespace Application;

public abstract class ProductBaseService<T> : BaseAppService where T : class
{
    private readonly CachedItems _cachedItems;
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IGenericRepository<Product> _productsRepo;

    protected ProductBaseService(IGenericRepository<Product> productsRepo,
    CachedItems cachedItems, IMapper mapper) : base(mapper)
    {
        _productsRepo = productsRepo;
        _cachedItems = cachedItems;
    }

    protected ProductBaseService(IGenericRepository<Product> productsRepo,
        IGenericRepository<Category> categoryRepo,
        IPhotoService photoService,
        CachedItems cachedItems, IMapper mapper, StoreContext context) : base(mapper, context)
    {
        _productsRepo = productsRepo;
        _categoryRepo = categoryRepo;
        _photoService = photoService;
        _cachedItems = cachedItems;
    }

    protected IQueryable<Product> FilteredProducts { get; set; }
    protected IQueryable<Product> PagedAndfilteredProducts { get; set; }
    protected ProductSpecParams ProductParams { get; set; }

    public async Task<Pagination<T>> GetProducts(ProductSpecParams productParams)
    {
        ProductParams = productParams;
        CalculateMaxMinVal(productParams);

        FilteredProducts = _productsRepo.GetAll()
            .WhereIf(productParams.MaxValue.HasValue, x => x.Currency == CurrencyCode.USD ? x.Price * _cachedItems.Currency.Try < productParams.MaxValue
                : x.Currency == CurrencyCode.EUR ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try < productParams.MaxValue
                : x.Currency == CurrencyCode.GBP ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try < productParams.MaxValue
                : x.Price < productParams.MaxValue)
            .WhereIf(productParams.MinValue.HasValue, x => x.Currency == CurrencyCode.USD ? x.Price * _cachedItems.Currency.Try > productParams.MinValue
                : x.Currency == CurrencyCode.EUR ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try > productParams.MinValue
                : x.Currency == CurrencyCode.GBP ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try > productParams.MinValue
                : x.Price > productParams.MinValue)
            .WhereIf(!string.IsNullOrEmpty(productParams.Search), p => p.Name.ToLower().Contains(productParams.Search))
            .WhereIf(productParams.CountyId.HasValue, p => p.County.Id == productParams.CountyId)
            .WhereIf(productParams.CityId.HasValue, p => p.County.CityId == productParams.CityId)
            .WhereIf(!productParams.GetAllStatus.HasValue, p => p.IsActive) //true: All, false: InActive, null: Active
            .WhereIf(productParams.GetAllStatus.HasValue && productParams.GetAllStatus == false, p => !p.IsActive)
            .WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId);

        AddCategoryFiltering();

        if (!string.IsNullOrEmpty(productParams.CategoryName))
        {
            List<int> categoryIds = await GetCategoryIds(productParams.CategoryName);
            if (categoryIds.Count > 0)
                FilteredProducts = FilteredProducts.Where(x => categoryIds.Contains(x.CategoryId));
        }

        PagedAndfilteredProducts = FilteredProducts
            .EFBigOrderBy(productParams.Sort, _cachedItems)
            .EFBigPageBy(productParams);

        var catGrpCountList = FilteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount
            {
                CategoryId = n.Key,
                Count = n.Count()
            }).ToList();

        var totalItems = catGrpCountList.Count == 0 ?
            0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);

        List<T> data = await QueryDatabase();

        return new Pagination<T>(productParams.PageIndex, productParams.PageSize, catGrpCountList, totalItems, data);
    }

    public async Task<object> GetActiveInactiveProducts(ProductSpecParams productParams)
    {
        var activeProducts = await _productsRepo.GetAll()
            .WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId)
            .Where(x => x.IsActive).CountAsync();

        var inactiveProducts = await _productsRepo.GetAll()
            .WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId)
            .Where(x => !x.IsActive).CountAsync();

        return new { activeProducts, inactiveProducts };
    }

    public async Task<ProductDetailDto> GetProduct(int id)
    {
        var product = await _productsRepo.GetAll()
            .ProjectTo<ProductDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");

        switch (product.Currency)
        {
            case CurrencyCode.USD:
                product.PriceText = $"{product.Price} USD";
                break;
            case CurrencyCode.EUR:
                product.PriceText = $"{product.Price} EUR";
                break;
            case CurrencyCode.GBP:
                product.PriceText = $"{product.Price} GBP";
                break;
            case CurrencyCode.TRY:
                product.PriceText = $"{product.Price} TL";
                break;
        }

        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        Product productObj = await _productsRepo.GetByIdAsync(product.Id);
        _mapper.Map(product, productObj);
        return await _productsRepo.SaveChangesAsync();
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int productId)
    {
        var product = await _productsRepo.GetAll().FirstOrDefaultAsync(x => x.Id == productId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) throw new ApiException(result.Error.Message);

        var photo = new ProductPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if (product.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        product.Photos.Add(photo);

        if (await _productsRepo.SaveChangesAsync())
        {
            return _mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }

    protected abstract void AddCategoryFiltering();
    protected abstract Task<List<T>> QueryDatabase();

    private void CalculateMaxMinVal(ProductSpecParams productParams)
    {
        if (productParams.MinValue.HasValue)
        {
            productParams.MinValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MinValue * (int)_cachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MinValue / (int)_cachedItems.Currency.Eur * (int)_cachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MinValue / (int)_cachedItems.Currency.Gbp * (int)_cachedItems.Currency.Try),
                CurrencyCode.TRY => (int)((decimal)productParams.MinValue),
                _ => productParams.MinValue,
            };
        }

        if (productParams.MaxValue.HasValue)
        {
            productParams.MaxValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MaxValue * _cachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MaxValue / _cachedItems.Currency.Eur * _cachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MaxValue / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try),
                CurrencyCode.TRY => (int)((decimal)productParams.MaxValue),
                _ => productParams.MaxValue,
            };
        }
    }
    private async Task<List<int>> GetCategoryIds(string categoryName)
    {
        if (_cachedItems.Categories.Count == 0)
            _cachedItems.Categories = await _categoryRepo.ListAllAsync();

        var selectedCategory = _cachedItems.Categories.First(x => x.Url == categoryName);
        List<int> categoryIds = new();
        FindChildCategories(selectedCategory);

        void FindChildCategories(Category category)
        {
            if (category.ChildCategories?.Count > 0)
            {
                foreach (var item in category.ChildCategories)
                {
                    FindChildCategories(item);
                }
            }
            else
            {
                categoryIds.Add(category.Id);
            }
        }

        return categoryIds;
    }
}