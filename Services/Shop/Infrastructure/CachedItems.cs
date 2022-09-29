using Core.Entities;
using Shop.Core.Dtos;

namespace Infrastructure;

public class CachedItems
{
    public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();
    public Currency Currency { get; set; } = new Currency();
    public List<CityDto> Cities { get; internal set; }
    public List<CountyDto> Counties { get; internal set; }
}