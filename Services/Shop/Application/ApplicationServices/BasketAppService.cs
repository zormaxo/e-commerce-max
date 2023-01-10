using System.Text.Json;
using AutoMapper;
using Core.Interfaces;
using Shop.Core.Entities;
using StackExchange.Redis;

namespace Shop.Application.ApplicationServices;

public class BasketAppService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    public BasketAppService(IBasketRepository basketRepository, IMapper mapper)
    {
        _mapper = mapper;
        _basketRepository = basketRepository;
    }

    public async Task<bool> DeleteBasketAsync(string basketId) { return await _basketRepository.DeleteBasketAsync(basketId); }

    public async Task<CustomerBasket> GetBasketAsync(string basketId)
    {
        return await _basketRepository.GetBasketAsync(basketId);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var customerBasket = _mapper.Map<CustomerBasket>(basket);

        return await _basketRepository.UpdateBasketAsync(customerBasket);
    }
}
