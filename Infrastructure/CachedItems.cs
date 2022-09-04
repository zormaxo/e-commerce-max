using Core.Entities;

namespace Infrastructure;

public class CachedItems
{
    public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();
    public Currency Currency { get; set; } = new Currency();
}