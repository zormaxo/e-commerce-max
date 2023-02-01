using Core.Entities.OrderAggregate;
using Shop.Core.Entities.OrderAggregate;

namespace Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(string buyerEmail, int delieveryMethod, string basketId, Address shippingAddress);

    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

    Task<Order> GetOrderByIdAsync(int id, string buyerEmail);

    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
}