using Shop.Core.Entities;
using Shop.Core.Interfaces;

namespace Shop.Application.ApplicationServices;

public class BasketAppService : BaseAppService
{
    private readonly IBasketRepository _basketRepository;

    public BasketAppService(IBasketRepository basketRepository, IServiceProvider serviceProvider) : base(serviceProvider)
    { _basketRepository = basketRepository; }

    public async Task<bool> DeleteBasketAsync(string basketId) { return await _basketRepository.DeleteBasketAsync(basketId); }

    public async Task<CustomerBasket> GetBasketAsync(string basketId) { return await _basketRepository.GetBasketAsync(basketId); }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var customerBasket = Mapper.Map<CustomerBasket>(basket);

        return await _basketRepository.UpdateBasketAsync(customerBasket);
    }
}
