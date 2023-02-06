using Shop.Core.Entities;
using Shop.Core.Entities.OrderAggregate;

namespace Shop.Application.Common.Interfaces.Payment;

public interface IPaymentService
{
    Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

    Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

    Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
}
