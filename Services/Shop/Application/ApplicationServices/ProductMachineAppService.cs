using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Shared.Dtos.Product;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMachineAppService : ProductBaseService<ProductMachineDto>
{
    IGenericRepository<ProductMachine> _machineRepo;

    public ProductMachineAppService(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductMachine> machineRepo,
        IGenericRepository<Category> categoryRepo,
        CachedItems cachedItems,
        IMapper mapper) : base(productsRepo, categoryRepo, cachedItems, mapper)
    { _machineRepo = machineRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _machineRepo.GetAll().WhereIf(ProductParams.IsNew.HasValue, p => p.IsNew == ProductParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }

    protected async override Task<List<ProductMachineDto>> QueryDatabase()
    {
        return await PagedAndFilteredProducts.ProjectTo<ProductMachineDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }
}