using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMachineAppService : ProductBaseService<ProductMachineDto>
{
    readonly IGenericRepository<ProductMachine> _machineRepo;

    public ProductMachineAppService(
        IGenericRepository<ProductMachine> machineRepo,
        CachedItems cachedItems,
        IMapper mapper,
        StoreContext storeContext) : base(mapper, storeContext, cachedItems)
    { _machineRepo = machineRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _machineRepo.GetAll()
            .WhereIf(ProductSpecParams.IsNew.HasValue, p => p.IsNew == ProductSpecParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }
}