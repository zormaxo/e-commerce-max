using Shop.Core.Shared.Dtos.City;

namespace Shop.Core.Shared.Dtos.Product;

public class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}