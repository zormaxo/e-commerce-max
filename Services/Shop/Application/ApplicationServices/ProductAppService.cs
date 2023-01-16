using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;
using System.Net;

namespace Shop.Application.ApplicationServices;

public class ProductAppService : ProductBaseService<ProductDto>
{
    public ProductAppService(StoreContext storeContext, CachedItems cachedItems, IMapper mapper) : base(
        mapper,
        storeContext,
        cachedItems)
    {
    }

    public async Task<ProductDetailDto> GetProduct(int id, int? userId)
    {
        var product = await StoreContext.Products
            .ProjectTo<ProductProjectDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");

        product.IsFavourite = product.Favourites.Any(x => x.UserId == userId);

        return Mapper.Map<ProductDetailDto>(product);
    }

    public async Task<object> GetActiveInactiveProducts(ProductSpecParams productParams)
    {
        var productsByUser = await StoreContext.Products
            .Where(p => p.UserId == productParams.UserId)
            .Select(x => x.IsActive)
            .ToListAsync();

        var activeProducts = productsByUser.Count(x => x);
        var inactiveProducts = productsByUser.Count(x => !x);
        var favourites = await StoreContext.Products
            .Where(y => y.Favourites.Any(x => x.UserId == productParams.UserId))
            .CountAsync();

        return new { activeProducts, inactiveProducts, favourites };
    }

    public async Task<int> ChangeActiveStatus(ProductActivateDto productActivateDto)
    {
        Product productObj = await StoreContext.Products.FindAsync(productActivateDto.Id);
        productObj.IsActive = productActivateDto.IsActive;
        return await StoreContext.SaveChangesAsync();
    }

    public async Task AddRemoveFavourite(int productId, int? userId)
    {
        if (userId is null)
            throw new ApiException("User not logged in");

        var fav = await StoreContext.Favourites.FindAsync(productId, userId);

        if (fav == null)
        {
            await StoreContext.Favourites.AddAsync(new Favourite { LikedProductId = productId, UserId = userId.Value });
        }
        else
        {
            StoreContext.Favourites.Remove(fav);
        }

        await StoreContext.SaveChangesAsync();
    }

    protected override void AddCategoryFiltering()
    {
    }
}