using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class ProductMachineAppService : ProductBaseService
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
        FilteredProducts = FilteredProducts.Include(x => x.ProductMachine)
               .WhereIf(ProductParams.IsNew.HasValue, p => p.ProductMachine.IsNew == ProductParams.IsNew);
    }

}