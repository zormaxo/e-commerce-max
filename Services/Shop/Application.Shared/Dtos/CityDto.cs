namespace Shop.Core.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CountyDto> Counties { get; set; }
    }
}
