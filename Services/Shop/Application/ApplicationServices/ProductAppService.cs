using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;
using Shop.Shared.Dtos.Product;
using System.Net;

namespace Shop.Application.ApplicationServices;

public class ProductAppService : ProductBaseService<ProductDetailDto>
{
    private readonly IPhotoService _photoService;
    public ProductAppService(IServiceProvider serviceProvider, IPhotoService photoService) : base(serviceProvider)
    { _photoService = photoService; }

    public async Task<ProductDetailDto> GetProduct(int id, int? userId)
    {
        var product = await StoreContext.Products
            .ProjectTo<ProductDetailDto>(Mapper.ConfigurationProvider)
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
            StoreContext.Favourites.Add(new Favourite { LikedProductId = productId, UserId = userId.Value });
        }
        else
        {
            StoreContext.Favourites.Remove(fav);
        }

        await StoreContext.SaveChangesAsync();
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int productId)
    {
        var product = await StoreContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            throw new ApiException(result.Error.Message);

        var photo = new ProductPhoto { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };

        if (product.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        product.Photos.Add(photo);

        if (await StoreContext.SaveChangesAsync() > 0)
        {
            return Mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }

    protected override void AddCategoryFiltering()
    {
    }
}