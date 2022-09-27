using Application.Entities;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Application.Specifications;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Net;

namespace Application;

public class ProductAppService : BaseAppService
{
    private readonly IPhotoService _photoService;
    private readonly IGenericRepository<Product> _productsRepo;

    public ProductAppService(IGenericRepository<Product> productsRepo,
        IPhotoService photoService, IMapper mapper, StoreContext context) : base(mapper, context)
    {
        _productsRepo = productsRepo;
        _photoService = photoService;
    }

    public async Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams)
    {
        var filteredProducts = _productsRepo.GetAll()
            .Include(x => x.Photos.Where(y => y.IsMain))
            .WhereIf(!productParams.GetAllStatus.HasValue, p => p.IsActive) //true: All, false: InActive, null: Active
            .WhereIf(productParams.GetAllStatus.HasValue && productParams.GetAllStatus == false, p => !p.IsActive);

        var catGrpCountList = filteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount
            {
                CategoryId = n.Key,
                Count = n.Count()
            }).ToList();

        var totalItems = catGrpCountList.Count == 0 ?
            0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);

        var products = await filteredProducts.ToListAsync();

        var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

        return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, catGrpCountList, totalItems, data);
    }

    public async Task<ProductToReturnDto> GetProduct(int id)
    {
        var product = await _productsRepo.GetAll()
            .Include(x => x.Category)
            .Include(x => x.Photos)
            .Include(x => x.User)
            .Include(x => x.County).ThenInclude(x => x.City)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new ApiException(HttpStatusCode.NotFound, $"Product with id: {id} is not found.");
        return _mapper.Map<ProductToReturnDto>(product);
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
}