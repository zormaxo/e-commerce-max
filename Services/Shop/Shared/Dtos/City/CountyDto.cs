namespace Shop.Shared.Dtos.City;

public class CountyDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public CityDto City { get; set; } = null!;
}
