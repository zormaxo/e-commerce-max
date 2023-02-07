using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.Entities.OrderAggregate;
using Shop.Core.Interfaces;

namespace Shop.Application.ApplicationServices;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;
    readonly IStoreContext _storeContext;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork, IStoreContext storeContext)
    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
        _storeContext = storeContext;
    }

    public async Task<Order?> CreateOrderAsync(
        string buyerEmail,
        int deliveryMethodId,
        string basketId,
        Core.Entities.OrderAggregate.Address shippingAddress)
    {
        // get basket from repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        // get items from the product repo
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _storeContext.Products.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == item.Id);
            var itemOrdered = new ProductItemOrdered(
                productItem.Id,
                productItem.Name,
                productItem.Photos.FirstOrDefault(x => x.IsMain)!.Url);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        // get delivery method from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        // calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);

        // check to see if order exists
        var order = _storeContext.Orders.FirstOrDefault(x => x.PaymentIntentId == basket.PaymentIntentId);

        if (order != null)
        {
            order.ShipToAddress = shippingAddress;
            order.DeliveryMethod = deliveryMethod!;
            order.Subtotal = subtotal;
            _unitOfWork.Repository<Order>().Update(order);
        }
        else
        {
            // create order
            order = new Order(items, buyerEmail, shippingAddress, deliveryMethod!, subtotal, basket.PaymentIntentId);
            _unitOfWork.Repository<Order>().Add(order);
        }

        // save to db
        var result = await _unitOfWork.Complete();

        if (result <= 0)
            return null;

        // return order
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    { return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync(); }

    public async Task<Order?> GetOrderByIdAsync(int id, string buyerEmail)
    {
        return await _storeContext.Orders
            .Include(x => x.OrderItems)
            .Include(x => x.DeliveryMethod)
            .FirstOrDefaultAsync(o => o.Id == id && o.BuyerEmail == buyerEmail);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        return await _storeContext.Orders
            .Include(x => x.OrderItems)
            .Include(x => x.DeliveryMethod)
            .Where(o => o.BuyerEmail == buyerEmail)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }
}