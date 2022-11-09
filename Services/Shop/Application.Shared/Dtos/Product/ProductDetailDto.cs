namespace Shop.Application.Shared.Dtos.Product;

public class ProductDetailDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;

    public CategoryDto Category { get; set; }

    public CountyDto County { get; set; }

    public ICollection<PhotoDto> Photos { get; set; }

    public string CreatedDate { get; set; } = string.Empty;

    public ProductMemberDto User { get; set; }

    //For shopping
    public string PictureUrl { get; set; } = string.Empty;

    public decimal Price { get; set; }
}