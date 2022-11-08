using Application;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductMaterialAppService : ProductBaseService<ProductMaterialDto>
{
    public ProductMaterialAppService(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<Category> categoryRepo,
        IPhotoService photoService,
        CachedItems cachedItems,
        IMapper mapper,
        StoreContext context) : base(productsRepo, categoryRepo, photoService, cachedItems, mapper, context)
    {
    }

    protected override void AddCategoryFiltering()
    {
        FilteredProducts = FilteredProducts.WhereIf(
            ProductParams.IsNew.HasValue,
            p => p.ProductMaterial.IsNew == ProductParams.IsNew);
    }

    protected async override Task<List<ProductMaterialDto>> QueryDatabase()
    {
        return await PagedAndFilteredProducts.ProjectTo<ProductMaterialDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }
}