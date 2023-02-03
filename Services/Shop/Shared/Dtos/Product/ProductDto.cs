using Shop.Core.Shared.Dtos;

namespace Shop.Shared.Dtos.Product;

//not used after angular shop.service.ts cache implemented
public class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string PictureUrl { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty; //For kuyumdan

    public decimal Price { get; set; } //For ecommmerce

    public DateTime CreatedDate { get; set; }
}