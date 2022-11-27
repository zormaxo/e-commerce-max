using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Persistence;

public class StoreContextSeed
{
    protected StoreContextSeed()
    {
    }

    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger<StoreContextSeed>();
        logger.LogInformation("Seeding starting...");

        try
        {
            if (!await context.Cities.AnyAsync())
            {
                logger.LogInformation("City seeding starting...");
                var cityData = await File.ReadAllTextAsync("../Infrastructure/SeedData/cities.json");
                var cities = JsonConvert.DeserializeObject<List<City>>(cityData);

                context.Cities.AddRange(cities);

                await context.SaveChangesAsync();
            }

            if (!await context.Counties.AnyAsync())
            {
                logger.LogInformation("County seeding starting...");
                var countyData = await File.ReadAllTextAsync("../Infrastructure/SeedData/counties.json");
                var counties = JsonConvert.DeserializeObject<List<County>>(countyData);

                context.Counties.AddRange(counties);

                await context.SaveChangesAsync();
            }

            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("User seeding starting...");
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

            if (!await context.Categories.AnyAsync())
            {
                logger.LogInformation("Category seeding starting...");
                var typesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/categories.json");
                var types = JsonConvert.DeserializeObject<List<Category>>(typesData);

                context.Categories.AddRange(types);

                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync())
            {
                logger.LogInformation("Product seeding starting...");
                var productsData = await File.ReadAllTextAsync("../Infrastructure/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }

            if (!await context.ProductMachines.AnyAsync())
            {
                logger.LogInformation("ProductMachine seeding starting...");
                var machinesData = await File.ReadAllTextAsync("../Infrastructure/SeedData/productMachines.json");
                var machines = JsonConvert.DeserializeObject<List<ProductMachine>>(machinesData);

                context.ProductMachines.AddRange(machines);

                await context.SaveChangesAsync();
            }

            logger.LogInformation("Seeding finished.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }
}