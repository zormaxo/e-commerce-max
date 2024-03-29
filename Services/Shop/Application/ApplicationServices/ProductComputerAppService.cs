using Shop.Application.Common.Helpers;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductComputerAppService : ProductBaseService<ProductComputerDto>
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