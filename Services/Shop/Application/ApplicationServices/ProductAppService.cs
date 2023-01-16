using AutoMapper;
using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductAppService : ProductBaseService<ProductDto>
{
    public ProductAppService(StoreContext storeContext, CachedItems cachedItems, IMapper mapper) : base(
        cachedItems,
        mapper,
        storeContext)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }
}