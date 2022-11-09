using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Application.Shared.Dtos.Product;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMachineAppService : ProductBaseService<ProductMachineDto>
{
    public ProductMachineAppService(
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