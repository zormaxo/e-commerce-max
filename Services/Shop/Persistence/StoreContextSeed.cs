using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.Core.Entities;
using Shop.Core.Entities.Identity;

namespace Shop.Persistence;

public static class StoreContextSeed
{
    public static async Task SeedAsync(
        StoreContext context,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("StoreContextSeed");
        logger.LogInformation("Seeding starting...");

        try
        {
            if (!await context.Cities.AnyAsync())
            {
                logger.LogInformation("City seeding starting...");
                var cityData = await File.ReadAllTextAsync("../Persistence/SeedData/cities.json");
                var cities = JsonConvert.DeserializeObject<List<City>>(cityData);

                context.Cities.AddRange(cities);

                await context.SaveChangesAsync();
            }

            if (!await context.Counties.AnyAsync())
            {
                logger.LogInformation("County seeding starting...");
                var countyData = await File.ReadAllTextAsync("../Persistence/SeedData/counties.json");
                var counties = JsonConvert.DeserializeObject<List<County>>(countyData);

                context.Counties.AddRange(counties);

                await context.SaveChangesAsync();
            }

            if (!await userManager.Users.AnyAsync())
            {
                var userData = await File.ReadAllTextAsync("../Persistence/SeedData/users.json");
                var users = JsonConvert.DeserializeObject<List<AppUser>>(userData);
                if (users == null)
                    return;

                var roles = new List<AppRole>
                {
                    new AppRole { Name = "Member" },
                    new AppRole { Name = "Admin" },
                    new AppRole { Name = "Moderator" },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    await userManager.CreateAsync(user, "Pa55w0rd");
                    await userManager.AddToRoleAsync(user, "Member");
                }

                var admin = new AppUser { UserName = "admin" };

                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
            }

            if (!await context.Categories.AnyAsync())
            {
                logger.LogInformation("Category seeding starting...");
                var typesData = await File.ReadAllTextAsync("../Persistence/SeedData/categories.json");
                var types = JsonConvert.DeserializeObject<List<Category>>(typesData);

                context.Categories.AddRange(types);

                await context.SaveChangesAsync();
            }

            if (!await context.Products.AnyAsync())
            {
                logger.LogInformation("Product seeding starting...");
                var productsData = await File.ReadAllTextAsync("../Persistence/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }

            if (!await context.ProductMachines.AnyAsync())
            {
                logger.LogInformation("ProductMachine seeding starting...");
                var machinesData = await File.ReadAllTextAsync("../Persistence/SeedData/productMachines.json");
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