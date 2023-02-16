using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces.Photo;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
using Shop.Shared.Dtos;
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
            .Include(x => x.County)
            .ThenInclude(x => x.City)
            .Include(x => x.Photos)
            .Include(x => x.User)
            .Include(x => x.Favourites)
            //.ProjectTo<ProductDetailDto>(Mapper.ConfigurationProvider)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");

        var productDetail = Mapper.Map<ProductDetailDto>(product);

        productDetail.IsFavourite = product.Favourites!.Any(x => x.UserId == userId);

        return productDetail;
    }

    [HttpPost]
    public async Task<Product> CreateProduct(ProductCreateDto productToCreate)
    {
        var product = Mapper.Map<ProductCreateDto, Product>(productToCreate);
        product.Photos = new List<ProductPhoto> { new ProductPhoto { IsMain = true, Url = "images/products/placeholder.png" } };

        StoreContext.Products.Add(product);
        int result = await StoreContext.SaveChangesAsync();

        if (result <= 0)
            throw new ApiException(HttpStatusCode.BadRequest, "Problem creating product");

        return product;
    }

    [HttpPut("{id}")]
    public async Task<Product> UpdateProduct(int id, ProductCreateDto productToUpdate)
    {
        var product = await StoreContext.Products.FindAsync(id);

        Mapper.Map(productToUpdate, product);

        int result = await StoreContext.SaveChangesAsync();

        if (result <= 0)
            throw new ApiException(HttpStatusCode.BadRequest, "Problem updating product");

        return product!;
    }

    [HttpDelete("{id}")]
    public async Task DeleteProduct(int id)
    {
        var product = await StoreContext.Products.FindAsync(id);

        StoreContext.Products.Remove(product!);

        int result = await StoreContext.SaveChangesAsync();

        if (result <= 0)
            throw new ApiException(HttpStatusCode.BadRequest, "Problem deleting product");
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
        Product? productObj = await StoreContext.Products!.FindAsync(productActivateDto.Id);
        productObj!.IsActive = productActivateDto.IsActive;
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

        if (product!.Photos.Count == 0)
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