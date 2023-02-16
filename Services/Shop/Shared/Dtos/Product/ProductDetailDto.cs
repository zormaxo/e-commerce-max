using Shop.Shared.Dtos.City;

namespace Shop.Shared.Dtos.Product;

public class ProductDetailDto : BaseProductDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public CountyDto County { get; set; } = null!;


    public DateTime CreatedDate { get; set; }

    public ProductMemberDto User { get; set; } = null!;

    //For e-commerce
    public decimal Price { get; set; }

    public int Currency { get; set; }
}