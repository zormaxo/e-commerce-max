using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
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
    private readonly StoreContext _storeContext;

    protected ProductBaseService(CachedItems cachedItems, IMapper mapper, StoreContext storeContext) : base(mapper)
    {
        _cachedItems = cachedItems;
        _storeContext = storeContext;
    }

    protected IQueryable<Product> FilteredProducts { get; set; }

    protected IQueryable<Product> PagedAndFilteredProducts { get; set; }

    protected ProductSpecParams ProductSpecParams { get; set; }

    public async Task<Pagination<Y>> GetProducts<Y>(ProductSpecParams productSpecParams) where Y : class
    {
        ProductSpecParams = productSpecParams;
        CalculateMaxMinVal(productSpecParams);

        FilteredProducts = _storeContext.Products
            .WhereIf(
                productSpecParams.MaxValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try < productSpecParams.MaxValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try < productSpecParams.MaxValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try < productSpecParams.MaxValue
                            : x.Price < productSpecParams.MaxValue)
            .WhereIf(
                productSpecParams.MinValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try > productSpecParams.MinValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try > productSpecParams.MinValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try > productSpecParams.MinValue
                            : x.Price > productSpecParams.MinValue)
            .WhereIf(!string.IsNullOrEmpty(productSpecParams.Search), p => p.Name.ToLower().Contains(productSpecParams.Search))
            .WhereIf(productSpecParams.CountyId.HasValue, p => p.County.Id == productSpecParams.CountyId)
            .WhereIf(productSpecParams.CityId.HasValue, p => p.County.CityId == productSpecParams.CityId)
            .WhereIf(!productSpecParams.GetAllStatus.HasValue, p => p.IsActive) //true: All, false: InActive, null: Active
            .WhereIf(productSpecParams.GetAllStatus.HasValue && productSpecParams.GetAllStatus == false, p => !p.IsActive)
            .WhereIf(
                productSpecParams.Favourite.HasValue && productSpecParams.Favourite == true,
                x => x.Favourites.Any(y => y.UserId == productSpecParams.UserId))
            .WhereIf(productSpecParams.UserId.HasValue, p => p.UserId == productSpecParams.UserId);

        AddCategoryFiltering();

        if (!string.IsNullOrEmpty(productSpecParams.CategoryName))
        {
            List<int> categoryIds = await GetCategoryIds(productSpecParams.CategoryName);
            if (categoryIds.Count > 0)
                FilteredProducts = FilteredProducts.Where(x => categoryIds.Contains(x.CategoryId));
        }

        var catGrpCountList = FilteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount { CategoryId = n.Key, Count = n.Count() })
            .ToList();

        var totalItems = catGrpCountList.Count == 0 ? 0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);

        PagedAndFilteredProducts = FilteredProducts
            .EFBigOrderBy(productSpecParams.Sort, _cachedItems)
            .EFBigPageBy(productSpecParams);

        List<Y> data = await PagedAndFilteredProducts
            .ProjectTo<Y>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        return new Pagination<Y>(productSpecParams.PageIndex, productSpecParams.PageSize, catGrpCountList, totalItems, data);
    }

    public async Task<ProductDetailDto> GetProduct(int id, int? userId)
    {
        var product = await _storeContext.Products
            .ProjectTo<ProductProjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");

        product.IsFavourite = product.Favourites.Any(x => x.UserId == userId);

        return _mapper.Map<ProductDetailDto>(product);
    }

    public async Task<object> GetActiveInactiveProducts(ProductSpecParams productParams)
    {
        var productsByUser = await _storeContext.Products
            .Where(p => p.UserId == productParams.UserId)
            .Select(x => x.IsActive)
            .ToListAsync();

        var activeProducts = productsByUser.Count(x => x);
        var inactiveProducts = productsByUser.Count(x => !x);
        var favourites = await _storeContext.Products
            .Where(y => y.Favourites.Any(x => x.UserId == productParams.UserId))
            .CountAsync();

        return new { activeProducts, inactiveProducts, favourites };
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        Product productObj = await _storeContext.Products.FindAsync(product.Id);
        _mapper.Map(product, productObj);
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangeActiveStatus(ProductActivateDto productActivateDto)
    {
        Product productObj = await _storeContext.Products.FindAsync(productActivateDto.Id);
        productObj.IsActive = productActivateDto.IsActive;
        return await _storeContext.SaveChangesAsync() > 0;
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int productId)
    {
        var product = await _storeContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            throw new ApiException(result.Error.Message);

        var photo = new ProductPhoto { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };

        if (product.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        product.Photos.Add(photo);

        if (await _storeContext.SaveChangesAsync() > 0)
        {
            return _mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }

    protected abstract void AddCategoryFiltering();

    private void CalculateMaxMinVal(ProductSpecParams productParams)
    {
        if (productParams.MinValue.HasValue)
        {
            productParams.MinValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MinValue * (int)_cachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MinValue / _cachedItems.Currency.Eur * _cachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MinValue / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try),
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
            _cachedItems.Categories = await _storeContext.Categories.ToListAsync();

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