using Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using Shop.Application;
using Shop.Core.Entities;

namespace Shop.API.Controllers
{
    public class BasketController : BaseApiController
    {

        private readonly BasketAppService _basketAppService;
        public BasketController(BasketAppService basketAppService)
        {
            _basketAppService = basketAppService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketAppService.GetBasketAsync(id);

            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            return await _basketAppService.UpdateBasketAsync(basket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketAppService.DeleteBasketAsync(id);
        }
    }
}
