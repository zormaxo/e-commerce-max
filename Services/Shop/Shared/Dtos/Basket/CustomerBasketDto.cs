using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Basket;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; } = string.Empty;

    public List<BasketItemDto>? Items { get; set; }

    public int? DeliveryMethodId { get; set; }

    public string? ClientSecret { get; set; } = string.Empty;

    public string? PaymentIntentId { get; set; } = string.Empty;

    public decimal ShippingPrice { get; set; }
}