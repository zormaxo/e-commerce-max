using Application.Entities;

namespace Application.Helpers;

public class CachedItems
{
    public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();
}