using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.Core.Entities;
using Shop.Core.Entities.Identity;
using Shop.Core.Entities.OrderAggregate;
using System.Reflection;

namespace Shop.Persistence;

public class StoreContextSeed
{
    private readonly StoreContext _context;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ILoggerFactory _loggerFactory;
    private readonly UserManager<AppUser> _userManager;

    public StoreContextSeed(
        StoreContext context,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ILoggerFactory loggerFactory)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _loggerFactory = loggerFactory;
    }

    public async Task SeedAsync()
    {
        var logger = _loggerFactory.CreateLogger("StoreContextSeed");
        logger.LogInformation("Seeding starting...");

        await _context.Database.MigrateAsync();
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        try
        {
            if (!await _context.Cities.AnyAsync())
            {
                logger.LogInformation("City seeding starting...");
                var cityData = await File.ReadAllTextAsync(path + "/SeedData/cities.json");
                var cities = JsonConvert.DeserializeObject<List<City>>(cityData);

                _context.Cities.AddRange(cities!);

                await _context.SaveChangesAsync();
            }

            if (!await _context.Counties.AnyAsync())
            {
                logger.LogInformation("County seeding starting...");
                var countyData = await File.ReadAllTextAsync(path + "/SeedData/counties.json");
                var counties = JsonConvert.DeserializeObject<List<County>>(countyData);

                _context.Counties.AddRange(counties!);

                await _context.SaveChangesAsync();
            }

            if (!await _userManager.Users.AnyAsync())
            {
                string userData = await File.ReadAllTextAsync(path + "/SeedData/users.json");
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
                    await _roleManager.CreateAsync(role);
                }

                foreach (var user in users)
                {
                    user.UserName = user.UserName!.ToLower();
                    await _userManager.CreateAsync(user, "1234");
                    await _userManager.AddToRoleAsync(user, "Member");
                }

                var admin = new AppUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                await _userManager.CreateAsync(admin, "1234");
                await _userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
            }

            if (!await _context.Categories.AnyAsync())
            {
                logger.LogInformation("Category seeding starting...");
                var typesData = await File.ReadAllTextAsync(path + "/SeedData/categories.json");
                var types = JsonConvert.DeserializeObject<List<Category>>(typesData);

                _context.Categories.AddRange(types!);

                await _context.SaveChangesAsync();
            }

            if (!await _context.Products.AnyAsync())
            {
                logger.LogInformation("Product seeding starting...");
                var productsData = await File.ReadAllTextAsync(path + "/SeedData/products.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

                foreach (var product in products!)
                    product.CreatedDate = GetRandomDate(DateTime.Now, DateTime.Now.AddDays(-365));

                _context.Products.AddRange(products);

                await _context.SaveChangesAsync();
            }
            if (!await _context.ProductVehicle.AnyAsync())
            {
                logger.LogInformation("ProductVehicle seeding starting...");
                var machinesData = await File.ReadAllTextAsync(path + "/SeedData/productVehicle.json");
                var machines = JsonConvert.DeserializeObject<List<ProductVehicle>>(machinesData);

                _context.ProductVehicle.AddRange(machines!);

                await _context.SaveChangesAsync();
            }
            if (!await _context.ProductComputer.AnyAsync())
            {
                logger.LogInformation("ProductComputer seeding starting...");
                var computerData = await File.ReadAllTextAsync(path + "/SeedData/productComputer.json");
                var computers = JsonConvert.DeserializeObject<List<ProductComputer>>(computerData);

                _context.ProductComputer.AddRange(computers!);

                await _context.SaveChangesAsync();
            }
            if (!await _context.ProductRealEstate.AnyAsync())
            {
                logger.LogInformation("ProductRealEstate seeding starting...");
                var realEstateDate = await File.ReadAllTextAsync(path + "/SeedData/productRealEstate.json");
                var realEstates = JsonConvert.DeserializeObject<List<ProductRealEstate>>(realEstateDate);

                _context.ProductRealEstate.AddRange(realEstates!);

                await _context.SaveChangesAsync();
            }
            if (!await _context.DeliveryMethods.AnyAsync())
            {
                logger.LogInformation("DeliveryMethod seeding starting...");
                var deliveryData = File.ReadAllText(path + "/SeedData/delivery.json");
                var methods = JsonConvert.DeserializeObject<List<DeliveryMethod>>(deliveryData);

                _context.DeliveryMethods.AddRange(methods!);

                await _context.SaveChangesAsync();
            }

            logger.LogInformation("Seeding finished.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }

    static readonly Random rnd = new();

    public static DateTime GetRandomDate(DateTime from, DateTime to)
    {
        var range = to - from;

        var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

        return DateTime.SpecifyKind(from + randTimeSpan, DateTimeKind.Utc);
    }
}