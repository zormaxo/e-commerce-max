using AutoMapper;
using Core.Entities;
using Infrastructure;
using Shop.Core.Dtos;

namespace Application;

public class CategoriesAppService : BaseAppService
{
    private readonly CachedItems _cachedItems;

    public CategoriesAppService(CachedItems cachedItems, IMapper mapper) : base(mapper)
    {
        _cachedItems = cachedItems;
    }

    public IReadOnlyList<CategoryDto> GetCategories()
    {
        var categories = _cachedItems.Categories.Where(x => x.Parent == null).ToList();
        List<CategoryDto> categoryDtos = new List<CategoryDto>();
        return _mapper.Map(categories, categoryDtos);
    }
}