using Shop.Shared.Dtos.Account;

namespace Shop.Shared.Dtos;

public class OrderToReturnDto
{
    public int Id { get; set; }

    public string BuyerEmail { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public AddressDto? ShipToAddress { get; set; }

    public string DeliveryMethod { get; set; } = string.Empty;

    public decimal ShippingPrice { get; set; }

    public IReadOnlyList<OrderItemDto>? OrderItems { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; } = string.Empty;
}