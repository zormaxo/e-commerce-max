using Shop.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductSemiFinishedAppService : ProductBaseService<ProductDto>
{
    public ProductSemiFinishedAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }
}