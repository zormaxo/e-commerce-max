namespace Shop.Shared.Dtos.City;

public class CityWithCountyDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<CountyDto>? Counties { get; set; }
}
