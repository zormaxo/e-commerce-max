using AutoMapper;
using Shop.Core.HelperTypes;
using Shop.Persistence;
using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductSemiFinishedAppService : ProductBaseService<ProductDto>
{
    public ProductSemiFinishedAppService(

        CachedItems cachedItems,
        IMapper mapper,
        StoreContext storeContext) : base(mapper, storeContext, cachedItems)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }
}