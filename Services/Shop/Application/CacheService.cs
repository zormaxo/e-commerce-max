using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestSharp;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared.Dtos;
using Shop.Persistence;

namespace Shop.Application;

public static class CacheService
{
    public async static Task FillCacheItemsAsync(
        StoreContext context,
        ILoggerFactory loggerFactory,
        IMapper mapper,
        CachedItems cachedItems,
        RestClient restClient)
    {
        var logger = loggerFactory.CreateLogger("CacheService");
        logger.LogInformation("Caching starting...");

        cachedItems.Categories = await context.Categories.OrderBy(x => x.Id).ToListAsync();
        cachedItems.Cities = await context.Cities.ToListAsync();
        cachedItems.Counties = await context.Counties.ToListAsync();

        var currencyObj = await context.Currency.FirstOrDefaultAsync(x => x.Date.Date == DateTime.UtcNow.Date);
        if (currencyObj == null)
        {
            logger.LogInformation($"{DateTime.UtcNow.ToShortDateString()} currency is null. Connecting to CurrencyFreak...");
            const string uri = $"https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD";

            //var client = new RestClient(uri);

            try
            {
                var fromDependency = await restClient.GetJsonAsync<CurrencyFreakDto>(uri);
                //var response3 = await client.GetJsonAsync<CurrencyFreakDto>(string.Empty);
                //var response2 = await client.GetAsync<CurrencyFreakDto>(new RestRequest());
                //var response = await client.GetAsync(new RestRequest());
                //var currencyDto = JsonConvert.DeserializeObject<CurrencyFreakDto>(response.Content);
                var currency = mapper.Map<Currency>(fromDependency);

                currency.Date = DateTime.UtcNow;
                cachedItems.Currency = currency;

                context.Add(currency);
                await context.SaveChangesAsync();
            }
            catch
            {
                logger.LogError($"Could not fetch data from CurrencyFreak...");
            }
        }
        else
        {
            logger.LogInformation($"Currency is exits");
            cachedItems.Currency = currencyObj;
        }

        logger.LogInformation("Caching finished");
    }
}
