using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Shared.Dtos.Basket;

namespace Shop.API.Controllers;

public class BasketController : BaseApiController
{
    private readonly BasketAppService _basketAppService;

    private readonly IMapper _mapper;

    public BasketController(BasketAppService basketAppService, IMapper mapper)
    {
        _basketAppService = basketAppService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await _basketAppService.GetBasketAsync(id);

        return basket ?? new CustomerBasket(id);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
    {
        var customerBasket = _mapper.Map<CustomerBasket>(basket);

        return await _basketAppService.UpdateBasketAsync(customerBasket);
    }

    [HttpDelete]
    public async Task DeleteBasketAsync(string id) { await _basketAppService.DeleteBasketAsync(id); }
}
