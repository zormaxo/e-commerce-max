namespace Shop.Core.Dtos.Product
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string PriceText { get; set; }
        public CountyDto County { get; set; }
        public string CreatedDate { get; set; }
        public string PictureUrl { get; set; }
    }
}