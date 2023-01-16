using AutoMapper;
using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos.Product;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductSemiFinishedAppService : ProductBaseService<ProductDto>
{
    public ProductSemiFinishedAppService(

        CachedItems cachedItems,
        IMapper mapper,
        StoreContext storeContext) : base(cachedItems, mapper, storeContext)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }
}