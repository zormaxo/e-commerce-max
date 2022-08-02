using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        protected StoreContextSeed()
        { }

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!await context.Cities.AnyAsync())
                {
                    var cityData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/cities.json");
                    var cities = JsonSerializer.Deserialize<List<City>>(cityData);

                    context.Cities.AddRange(cities);

                    await context.SaveChangesAsync();
                }

                if (!await context.Counties.AnyAsync())
                {
                    var countyData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/counties.json");
                    var counties = JsonSerializer.Deserialize<List<County>>(countyData);

                    context.Counties.AddRange(counties);

                    await context.SaveChangesAsync();
                }

                if (!await context.Users.AnyAsync())
                {
                    var userData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/users.json");
                    var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                    context.Users.AddRange(users);

                    foreach (var user in users)
                    {
                        using var hmac = new HMACSHA512();

                        user.Username = user.Username.ToLower();
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123"));

                        context.Users.Add(user);
                    }

                    await context.SaveChangesAsync();
                }

                if (!await context.ProductBrands.AnyAsync())
                {
                    var brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    context.ProductBrands.AddRange(brands);

                    await context.SaveChangesAsync();
                }

                if (!await context.Categories.AnyAsync())
                {
                    var typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/categories.json");
                    var types = JsonSerializer.Deserialize<List<Category>>(typesData);

                    context.Categories.AddRange(types);

                    await context.SaveChangesAsync();
                }

                if (!await context.Products.AnyAsync())
                {
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    context.Products.AddRange(products);

                    await context.SaveChangesAsync();
                }

                if (!await context.ProductMachines.AnyAsync())
                {
                    var machinesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/productMachines.json");
                    var machines = JsonSerializer.Deserialize<List<ProductMachine>>(machinesData);

                    context.ProductMachines.AddRange(machines);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}