namespace Shop.Application.Shared.Dtos.Product;

public class ProductDto : BaseDto
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public CountyDto County { get; set; }

    public string CreatedDate { get; set; }

    public string PictureUrl { get; set; }
}