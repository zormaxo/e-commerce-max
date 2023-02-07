using Shop.Shared.Dtos.Account;

namespace Shop.Shared.Dtos;

public class OrderDto
{
    public string BasketId { get; set; } = string.Empty;

    public int DeliveryMethodId { get; set; }

    public AddressDto ShipToAddress { get; set; } = new AddressDto();
}