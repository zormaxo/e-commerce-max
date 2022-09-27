using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class ProductMaterialAppService : ProductBaseService
{
    public ProductMaterialAppService(IGenericRepository<Product> productsRepo,
       IGenericRepository<Category> categoryRepo,
       IPhotoService photoService,
       CachedItems cachedItems,
       IMapper mapper,
       StoreContext context) : base(productsRepo, categoryRepo, photoService, cachedItems, mapper, context)
    {

    }

    protected override void AddCategoryFiltering()
    {
        FilteredProducts = FilteredProducts.Include(x => x.ProductMaterial)
               .WhereIf(ProductParams.IsNew.HasValue, p => p.ProductMaterial.IsNew == ProductParams.IsNew);
    }

}