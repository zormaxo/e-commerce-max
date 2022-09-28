using Application.Interfaces;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos.Product;

namespace Application;

public class ProductMachineAppService : ProductBaseService<ProductDetailDto>
{
    public ProductMachineAppService(IGenericRepository<Product> productsRepo,
       IGenericRepository<Category> categoryRepo,
       IPhotoService photoService,
       CachedItems cachedItems,
       IMapper mapper,
       StoreContext context) : base(productsRepo, categoryRepo, photoService, cachedItems, mapper, context)
    {

    }

    protected override void AddCategoryFiltering()
    {
        FilteredProducts = FilteredProducts
               .WhereIf(ProductParams.IsNew.HasValue, p => p.ProductMachine.IsNew == ProductParams.IsNew);
    }

    protected async override Task<List<ProductDetailDto>> QueryDatabase()
    {
        return await PagedAndfilteredProducts.ProjectTo<ProductDetailDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}