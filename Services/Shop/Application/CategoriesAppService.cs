using AutoMapper;
using Core.Entities;
using Infrastructure;

namespace Application;

public class CategoriesAppService : BaseAppService
{
    private readonly CachedItems _cachedItems;

    public CategoriesAppService(CachedItems cachedItems, IMapper mapper) : base(mapper)
    {
        _cachedItems = cachedItems;
    }

    public IReadOnlyList<Category> GetCategories()
    {
        return _cachedItems.Categories.Where(x => x.Parent == null).ToList();
    }
}