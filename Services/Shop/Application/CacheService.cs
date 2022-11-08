using Application;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Shop.Core.Dtos;

namespace Shop.Application
{
    public class CacheService
    {
        public async static Task FillCacheItems(
            StoreContext context,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            CachedItems cachedItems)
        {
            var logger = loggerFactory.CreateLogger<CacheService>();
            logger.LogInformation("Caching started");

            cachedItems.Categories = await context.Categories.ToListAsync();
            cachedItems.Cities = await context.Cities.ProjectTo<CityDto>(mapper.ConfigurationProvider).ToListAsync();
            cachedItems.Counties = await context.Counties.ProjectTo<CountyDto>(mapper.ConfigurationProvider).ToListAsync();

            var currencyObj = await context.Currency.FirstOrDefaultAsync(x => x.Date.Date == DateTime.UtcNow.Date);
            if (currencyObj == null)
            {
                var client = new RestClient(
                    "https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");
                var response = await client.GetAsync(new RestRequest());

                var currencyDto = JsonConvert.DeserializeObject<CurrencyFreakDto>(response.Content);
                var currency = mapper.Map<Currency>(currencyDto);

                cachedItems.Currency = currency;

                context.Add(currency);
                await context.SaveChangesAsync();
            }
            else
            {
                cachedItems.Currency = currencyObj;
            }

            logger.LogInformation("Caching finished");
        }
    }
}
