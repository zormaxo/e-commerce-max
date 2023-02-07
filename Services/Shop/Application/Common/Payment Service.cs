using Microsoft.Extensions.Configuration;
using Shop.Application.Common.Interfaces.Payment;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.Entities;
using Shop.Core.Entities.OrderAggregate;
using Shop.Core.Interfaces;
using Stripe;

namespace Shop.Application.Common;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBasketRepository _basketRepository;
    private readonly IConfiguration _config;
    readonly IStoreContext _storeContext;

    public PaymentService(
        IUnitOfWork unitOfWork,
        IBasketRepository basketRepository,
        IConfiguration config,
        IStoreContext storeContext)
    {
        _storeContext = storeContext;
        _config = config;
        _basketRepository = basketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

        var basket = await _basketRepository.GetBasketAsync(basketId);

        if (basket == null)
            return null;

        var shippingPrice = 0m;

        if (basket.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
            shippingPrice = deliveryMethod.Price;
        }

        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(item.Id);
            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }
        }

        var service = new PaymentIntentService();

        PaymentIntent intent;

        if (string.IsNullOrEmpty(basket.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };
            intent = await service.CreateAsync(options);
            basket.PaymentIntentId = intent.Id;
            basket.ClientSecret = intent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
            };
            await service.UpdateAsync(basket.PaymentIntentId, options);
        }

        await _basketRepository.UpdateBasketAsync(basket);

        return basket;
    }

    public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var order = _storeContext.Orders.FirstOrDefault(x => x.PaymentIntentId == paymentIntentId);

        if (order == null)
            return null;

        order.Status = OrderStatus.PaymentFailed;
        await _unitOfWork.Complete();

        return order;
    }

    public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var order = _storeContext.Orders.FirstOrDefault(x => x.PaymentIntentId == paymentIntentId);

        if (order == null)
            return null;

        order.Status = OrderStatus.PaymentReceived;
        await _unitOfWork.Complete();

        return order;
    }
}