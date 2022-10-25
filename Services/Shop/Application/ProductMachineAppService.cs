using Application.Interfaces;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos.Product;

namespace Application;

public class ProductMachineAppService : ProductBaseService<ProductMachineDto>
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

    protected async override Task<List<ProductMachineDto>> QueryDatabase()
    {
        return await PagedAndFilteredProducts.ProjectTo<ProductMachineDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }
}