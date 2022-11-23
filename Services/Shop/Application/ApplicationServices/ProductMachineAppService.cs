using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;
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
}