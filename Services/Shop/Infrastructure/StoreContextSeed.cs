using Application.Entities;
using AutoMapper;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace Application;

public class StoreContextSeed
{
    protected StoreContextSeed()
    {
    }

    public static async Task SeedAsync(
        StoreContext context,
        ILoggerFactory loggerFactory,
        IMapper mapper,
        CachedItems cachedItems)
    {
        try
        {
            if (!await context.Cities.AnyAsync())
            {
                var cityData = await File.ReadAllTextAsync("../Infrastructure/SeedData/cities.json");
                var cities = JsonConvert.DeserializeObject<List<City>>(cityData);

                context.Cities.AddRange(cities);

                await context.SaveChangesAsync();
            }

            if (!await context.Counties.AnyAsync())
            {
                var countyData = await File.ReadAllTextAsync("../Infrastructure/SeedData/counties.json");
                var counties = JsonConvert.DeserializeObject<List<County>>(countyData);

                context.Counties.AddRange(counties);

                await context.SaveChangesAsync();
            }

            if (!await context.Users.AnyAsync())
            {
                var userData = await File.ReadAllTextAsync("../Infrastructure/SeedData/users.json");
                var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);

                context.Users.AddRange(users);

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();

                    user.UserName = user.UserName.ToLower();
                    user.PasswordSalt = hmac.Key;
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123"));

                    context.Users.Add(user);
                }

                await context.SaveChangesAsync();
            }

            if (!await context.ProductBrands.AnyAsync())
            {
                var brandsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/brands.json");
                var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);

                context.ProductBrands.AddRange(brands);

                await context.SaveChangesAsync();
            }

            if (!await context.Categories.AnyAsync())
            {
                var typesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/categories.json");
                var types = JsonConvert.DeserializeObject<List<Category>>(typesData);

                context.Categories.AddRange(types);

                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }

            if (!await context.ProductMachines.AnyAsync())
            {
                var machinesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/productMachines.json");
                var machines = JsonConvert.DeserializeObject<List<ProductMachine>>(machinesData);

                context.ProductMachines.AddRange(machines);

                await context.SaveChangesAsync();
            }

            var currencyObj = await context.Currency.FirstOrDefaultAsync(x => x.Date.Date == DateTime.UtcNow.Date);
            if (currencyObj == null)
            {
                var client = new RestClient(
                    "https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");
                var response = await client.GetAsync(new RestRequest());

                var currency = JsonConvert.DeserializeObject<Currency>(response.Content);

                cachedItems.Currency = currency;
                context.Add(currency);
                await context.SaveChangesAsync();
            }
            else
            {
                cachedItems.Currency = currencyObj;
            }

            cachedItems.Categories = await context.Categories.AsNoTracking().ToListAsync();
            cachedItems.Cities = await context.Cities.AsNoTracking().ToListAsync();
            cachedItems.Counties = await context.Counties.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}