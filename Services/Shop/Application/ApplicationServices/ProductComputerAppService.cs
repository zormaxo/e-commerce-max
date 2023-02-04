using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductComputerAppService : ProductBaseService<ProductMaterialDto>
{
    readonly IGenericRepository<ProductComputer> _materialRepo;

    public ProductComputerAppService(IGenericRepository<ProductComputer> materialRepo, IServiceProvider serviceProvider) : base(
        serviceProvider)
    { _materialRepo = materialRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _materialRepo.GetAll()
            .WhereIf(ProductSpecParams.IsNew.HasValue, p => p.IsNew == ProductSpecParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }
}