using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMaterialAppService : ProductBaseService<ProductMaterialDto>
{
    readonly IGenericRepository<ProductMaterial> _materialRepo;

    public ProductMaterialAppService(
        IGenericRepository<ProductMaterial> materialRepo,
        CachedItems cachedItems,
        IMapper mapper,
        StoreContext storeContext) : base(cachedItems, mapper, storeContext)
    { _materialRepo = materialRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _materialRepo.GetAll()
            .WhereIf(ProductSpecParams.IsNew.HasValue, p => p.IsNew == ProductSpecParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }
}