using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shop.Application;
using Shop.Core.HelperTypes;
using Shop.Persistence;

namespace Shop.API;

public class Program
{
    protected Program()
    {
    }

    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        Log.Information("Application Starting Up");

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<StoreContext>();
            var mapper = services.GetRequiredService<IMapper>();
            var cahcedItems = services.GetRequiredService<CachedItems>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);
            await CacheService.FillCacheItemsAsync(context, loggerFactory, mapper, cahcedItems);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .UseSerilog((context, _, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
        .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}