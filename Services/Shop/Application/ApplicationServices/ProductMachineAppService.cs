using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMachineAppService : ProductBaseService<ProductMachineDto>
{
    readonly IGenericRepository<ProductVehicle> _machineRepo;

    public ProductMachineAppService(IGenericRepository<ProductVehicle> machineRepo, IServiceProvider serviceProvider) : base(
        serviceProvider)
    { _machineRepo = machineRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _machineRepo.GetAll()
            .WhereIf(ProductSpecParams.IsNew.HasValue, p => p.IsNew == ProductSpecParams.IsNew);

        //FilteredProducts = from products in FilteredProducts
        //            join cat in categoryFilter on products.Id equals cat.Id
        //    select new ProductMachine { Id = cat.Id, IsDeleted = cat.IsDeleted, IsNew = cat.IsNew, Product = products };


        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }
}