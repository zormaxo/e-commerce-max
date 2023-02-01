using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos;

namespace Shop.Application.ApplicationServices;

public class CategoriesAppService : BaseAppService
{
    public CategoriesAppService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public IReadOnlyList<CategoryDto> GetCategories()
    {
        var categories = CachedItems.Categories.Where(x => x.Parent == null).ToList();
        List<CategoryDto> categoryDtos = new();
        return Mapper.Map(categories, categoryDtos);
    }
}