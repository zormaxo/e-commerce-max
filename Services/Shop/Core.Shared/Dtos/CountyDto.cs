namespace Shop.Core.Dtos
{
    public class CountyDto : BaseDto
    {
        public string Name { get; set; }
        public CityDto City { get; set; }
    }
}
