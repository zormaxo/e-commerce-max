using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Persistence;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public abstract class ProductBaseService<T> : BaseAppService where T : class
{
    protected ProductBaseService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected IQueryable<Product> FilteredProducts { get; set; }

    protected IQueryable<Product> PagedAndFilteredProducts { get; set; }

    protected ProductSpecParams ProductSpecParams { get; set; }

    public async Task<Pagination<Y>> GetProducts<Y>(ProductSpecParams productSpecParams) where Y : BaseProductDto
    {
        ProductSpecParams = productSpecParams;
        CalculateMaxMinVal(productSpecParams);

        FilteredProducts = StoreContext.Products
            .WhereIf(
                productSpecParams.MaxValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * CachedItems.Currency.Try < productSpecParams.MaxValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / CachedItems.Currency.Eur * CachedItems.Currency.Try < productSpecParams.MaxValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / CachedItems.Currency.Gbp * CachedItems.Currency.Try < productSpecParams.MaxValue
                            : x.Price < productSpecParams.MaxValue)
            .WhereIf(
                productSpecParams.MinValue.HasValue,
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * CachedItems.Currency.Try > productSpecParams.MinValue
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / CachedItems.Currency.Eur * CachedItems.Currency.Try > productSpecParams.MinValue
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / CachedItems.Currency.Gbp * CachedItems.Currency.Try > productSpecParams.MinValue
                            : x.Price > productSpecParams.MinValue)
            .WhereIf(!string.IsNullOrEmpty(productSpecParams.Search), p => p.Name.ToLower().Contains(productSpecParams.Search))
            .WhereIf(productSpecParams.CountyId.HasValue, p => p.County.Id == productSpecParams.CountyId)
            .WhereIf(productSpecParams.CityId.HasValue, p => p.County.CityId == productSpecParams.CityId)
            .WhereIf(!productSpecParams.GetAllStatus.HasValue, p => p.IsActive) //true: All, false: InActive, null: Active
            .WhereIf(productSpecParams.GetAllStatus.HasValue && productSpecParams.GetAllStatus == false, p => !p.IsActive);

        if (productSpecParams.Favourite.HasValue && productSpecParams.Favourite == true)
        {
            FilteredProducts = FilteredProducts.Where(x => x.Favourites.Any(y => y.UserId == productSpecParams.UserId));
        }
        else if (productSpecParams.UserId.HasValue)
        {
            FilteredProducts = FilteredProducts.Where(p => p.UserId == productSpecParams.UserId);
        }


        AddCategoryFiltering();

        if (!string.IsNullOrEmpty(productSpecParams.CategoryName))
        {
            List<int> categoryIds = await GetCategoryIds(productSpecParams.CategoryName);
            if (categoryIds.Count > 0)
                FilteredProducts = FilteredProducts.Where(x => categoryIds.Contains(x.CategoryId));
        }

        var catGrpCountList = FilteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount { CategoryId = n.Key, Count = n.Count() })
            .AsNoTracking()
            .ToList();

        var totalItems = catGrpCountList.Count == 0 ? 0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);

        PagedAndFilteredProducts = FilteredProducts
            .EFBigOrderBy(productSpecParams.Sort, CachedItems)
            .EFBigPageBy(productSpecParams);

        List<Y> data = await PagedAndFilteredProducts
            .ProjectTo<Y>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();

        data.ForEach(x => x.IsFavourite = x.Favourites.Any(y => y.UserId == UserId));

        return new Pagination<Y>(productSpecParams.PageIndex, productSpecParams.PageSize, catGrpCountList, totalItems, data);
    }

    public async Task<int> UpdateProduct(Product product)
    {
        Product productObj = await StoreContext.Products.FindAsync(product.Id);
        Mapper.Map(product, productObj);
        return await StoreContext.SaveChangesAsync();
    }

    protected abstract void AddCategoryFiltering();

    private void CalculateMaxMinVal(ProductSpecParams productParams)
    {
        if (productParams.MinValue.HasValue)
        {
            productParams.MinValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MinValue * (int)CachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MinValue / CachedItems.Currency.Eur * CachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MinValue / CachedItems.Currency.Gbp * CachedItems.Currency.Try),
                CurrencyCode.TRY => (int)(decimal)productParams.MinValue,
                _ => productParams.MinValue,
            };
        }

        if (productParams.MaxValue.HasValue)
        {
            productParams.MaxValue = productParams.Currency switch
            {
                CurrencyCode.USD => (int)((decimal)productParams.MaxValue * CachedItems.Currency.Try),
                CurrencyCode.EUR => (int)((decimal)productParams.MaxValue / CachedItems.Currency.Eur * CachedItems.Currency.Try),
                CurrencyCode.GBP => (int)((decimal)productParams.MaxValue / CachedItems.Currency.Gbp * CachedItems.Currency.Try),
                CurrencyCode.TRY => (int)(decimal)productParams.MaxValue,
                _ => productParams.MaxValue,
            };
        }
    }

    private async Task<List<int>> GetCategoryIds(string categoryName)
    {
        if (CachedItems.Categories.Count == 0)
            CachedItems.Categories = await StoreContext.Categories.ToListAsync();

        var selectedCategory = CachedItems.Categories.First(x => x.Url == categoryName);
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