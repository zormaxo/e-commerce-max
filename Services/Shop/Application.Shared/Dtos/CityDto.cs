namespace Shop.Application.Shared.Dtos;

public class CityDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<CountyDto>? Counties { get; set; }
}
