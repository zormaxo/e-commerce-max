using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;
using System.Net;

namespace Shop.Application.ApplicationServices;

public abstract class ProductBaseService<T> : BaseAppService where T : class
{
    private readonly CachedItems _cachedItems;
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IGenericRepository<Product> _productsRepo;

    protected ProductBaseService(IGenericRepository<Product> productsRepo, CachedItems cachedItems, IMapper mapper) : base(mapper)
    {
        _productsRepo = productsRepo;
        _cachedItems = cachedItems;
    }

    protected ProductBaseService(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<Category> categoryRepo,
        CachedItems cachedItems,
        IMapper mapper) : base(mapper)
    {
        _productsRepo = productsRepo;
        _categoryRepo = categoryRepo;
        _cachedItems = cachedItems;
    }

    protected IQueryable<Product> FilteredProducts { get; set; }

    protected IQueryable<Product> PagedAndFilteredProducts { get; set; }

    protected ProductParams ProductParams { get; set; }

    public async Task<Pagination<T>> GetProducts(ProductParams productParams)
    {
        ProductParams = productParams;
        CalculateMaxMinVal(productParams);

        FilteredProducts = _productsRepo.GetAll()
            .WhereIf(
                productParams.MaxValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try < productParams.MaxValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try < productParams.MaxValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try < productParams.MaxValue
                            : x.Price < productParams.MaxValue)
            .WhereIf(
                productParams.MinValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try > productParams.MinValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try > productParams.MinValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try > productParams.MinValue
                            : x.Price > productParams.MinValue)
            .WhereIf(!string.IsNullOrEmpty(productParams.Search), p => p.Name.ToLower().Contains(productParams.Search))
            .WhereIf(productParams.CountyId.HasValue, p => p.County.Id == productParams.CountyId)
            .WhereIf(productParams.CityId.HasValue, p => p.County.CityId == productParams.CityId)
            .WhereIf(!productParams.GetAllStatus.HasValue, p => p.IsActive) //true: All, false: InActive, null: Active
            .WhereIf(productParams.GetAllStatus.HasValue && productParams.GetAllStatus == false, p => !p.IsActive);

        AddCategoryFiltering();

        if (productParams.Favourite)
        {
            FilteredProducts = FilteredProducts.Include(x => x.Favourites)
                .Where(x => x.Favourites.Any(y => y.UserId == productParams.UserId));
        }
        else
        {
            FilteredProducts = FilteredProducts.WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId);
        }

        if (!string.IsNullOrEmpty(productParams.CategoryName))
        {
            List<int> categoryIds = await GetCategoryIds(productParams.CategoryName);
            if (categoryIds.Count > 0)
                FilteredProducts = FilteredProducts.Where(x => categoryIds.Contains(x.CategoryId));
        }

        var catGrpCountList = FilteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount { CategoryId = n.Key, Count = n.Count() })
            .ToList();

        var totalItems = catGrpCountList.Count == 0 ? 0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);

        PagedAndFilteredProducts = FilteredProducts
            .EFBigOrderBy(productParams.Sort, _cachedItems)
            .EFBigPageBy(productParams);

        List<T> data = await PagedAndFilteredProducts
            .ProjectTo<T>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        return new Pagination<T>(productParams.PageIndex, productParams.PageSize, catGrpCountList, totalItems, data);
    }

    public async Task<object> GetActiveInactiveProducts(ProductParams productParams)
    {
        var productsByUser = await _productsRepo.GetAll()
            .Where(p => p.UserId == productParams.UserId)
            .Select(x => x.IsActive)
            .ToListAsync();

        var activeProducts = productsByUser.Count(x => x);
        var inactiveProducts = productsByUser.Count(x => !x);
        var favourites = await _productsRepo.GetAll()
            .Where(y => y.Favourites.Any(x => x.UserId == productParams.UserId))
            .CountAsync();

        return new { activeProducts, inactiveProducts, favourites };
    }

    public async Task<ProductDetailDto> GetProduct(int id, int? userId)
    {
        var product = await _productsRepo.GetAll()
            .ProjectTo<ProductProjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");

        product.IsFavourite = product.Favourites.Any(x => x.UserId == userId);

        return _mapper.Map<ProductDetailDto>(product);
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        Product productObj = await _productsRepo.GetByIdAsync(product.Id);
        _mapper.Map(product, productObj);
        return await _productsRepo.SaveChangesAsync();
    }

    public async Task<bool> ChangeActiveStatus(ProductActivateDto productActivateDto)
    {
        Product productObj = await _productsRepo.GetByIdAsync(productActivateDto.Id);
        productObj.IsActive = productActivateDto.IsActive;
        return await _productsRepo.SaveChangesAsync();
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int productId)
    {
        var product = await _productsRepo.GetAll().FirstOrDefaultAsync(x => x.Id == productId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            throw new ApiException(result.Error.Message);

        var photo = new ProductPhoto { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };

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

    private void CalculateMaxMinVal(ProductParams productParams)
    {
        if (productParams.MinValue.HasValue)
        {
            productParams.MinValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MinValue * (int)_cachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MinValue /
                    (int)_cachedItems.Currency.Eur *
                    (int)_cachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MinValue /
                    (int)_cachedItems.Currency.Gbp *
                    (int)_cachedItems.Currency.Try),
                CurrencyCode.TRY => (int)(decimal)productParams.MinValue,
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
                CurrencyCode.TRY => (int)(decimal)productParams.MaxValue,
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