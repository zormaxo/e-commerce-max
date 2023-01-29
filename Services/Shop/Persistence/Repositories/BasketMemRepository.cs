using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Entities;

namespace Shop.Persistence.Repositories
{
    public class BasketMemRepository : IBasketRepository
    {
        private readonly IMemoryCache _memoryCache;
        public BasketMemRepository(IMemoryCache memoryCache) { _memoryCache = memoryCache; }

        public Task<bool> DeleteBasketAsync(string basketId)
        {
            _memoryCache.Remove(basketId);
            return Task.FromResult(true);
        }

        public Task<CustomerBasket?> GetBasketAsync(string basketId)
        { return Task.FromResult(_memoryCache.Get<CustomerBasket>(basketId)); }

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            Guid guid = Guid.NewGuid();
            _memoryCache.Set(guid, basket, TimeSpan.FromDays(1));
            return GetBasketAsync(guid.ToString());
        }
    }
}