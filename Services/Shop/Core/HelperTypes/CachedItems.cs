using Core.Entities;

namespace Infrastructure;

public class CachedItems
{
    public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();

    public Currency Currency { get; set; } = new Currency();

    public List<City> Cities { get; set; }

    public List<County> Counties { get; set; }
}