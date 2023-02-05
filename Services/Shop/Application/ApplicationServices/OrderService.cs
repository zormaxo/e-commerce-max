using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.Entities.OrderAggregate;
using Shop.Core.Interfaces;

namespace Shop.Application.ApplicationServices;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IBasketRepository basketRepo, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
    {
        // get basket from repo
        var basket = await _basketRepo.GetBasketAsync(basketId);

        // get items from the product repo
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await _unitOfWork.Repository<Product>()
                .GetAll()
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(y => y.Id == item.Id);

            var omer1 = productItem.Photos.ToList();

            var omer = productItem.Photos.FirstOrDefault(x => x.IsMain);


            var itemOrdered = new ProductItemOrdered(
                productItem.Id,
                productItem.Name,
                productItem.Photos.FirstOrDefault(x => x.IsMain).Url);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        // get delivery method from repo
        var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        // calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);

        // create order
        var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
        _unitOfWork.Repository<Order>().Add(order);

        // save to db
        var result = await _unitOfWork.Complete();

        if (result <= 0)
            return null;

        // delete basket
        await _basketRepo.DeleteBasketAsync(basketId);

        // return order
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    { return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync(); }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        return await _unitOfWork.Repository<Order>()
            .GetAll()
            .Include(o => o.OrderItems)
            .Include(o => o.DeliveryMethod)
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync(o => o.Id == id && o.BuyerEmail == buyerEmail);


        // var spec = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

        // return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        //return null;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        return await _unitOfWork.Repository<Order>()
            .GetAll()
            .Where(o => o.BuyerEmail == buyerEmail)
            .Include(o => o.OrderItems)
            .Include(o => o.DeliveryMethod)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
        // var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

        // return await _unitOfWork.Repository<Order>().ListAsync(spec);
        //return null;
    }
}