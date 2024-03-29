using Shop.Application.Common.Helpers;
using Shop.Core.Entities;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductRealEstateAppService : ProductBaseService<ProductRealEstateDto>
{
    readonly IGenericRepository<ProductRealEstate> _semiFinishedRepo;
    public ProductRealEstateAppService(IGenericRepository<ProductRealEstate> semiFinishedRepo, IServiceProvider serviceProvider) : base(
        serviceProvider)
    { _semiFinishedRepo = semiFinishedRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _semiFinishedRepo.GetAll()
            .WhereIf(ProductSpecParams.IsNew.HasValue, p => p.IsNew == ProductSpecParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }
}