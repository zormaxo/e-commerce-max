using Shop.Application.Common.Helpers;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductVehicleAppService : ProductBaseService<ProductVehicleDto>
{
    readonly IGenericRepository<ProductVehicle> _machineRepo;

    public ProductVehicleAppService(IGenericRepository<ProductVehicle> machineRepo, IServiceProvider serviceProvider) : base(
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