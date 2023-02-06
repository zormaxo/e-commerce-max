using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.ApplicationServices;
using Shop.Application.SignalR;
using Shop.Core.HelperTypes;

namespace Shop.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<AccountAppService>();
        services.AddScoped<UserAppService>();
        services.AddScoped<ProductAppService>();
        services.AddScoped<ProductVehicleAppService>();
        services.AddScoped<ProductRealEstateAppService>();
        services.AddScoped<ProductComputerAppService>();
        services.AddScoped<CategoriesAppService>();
        services.AddScoped<BasketAppService>();

        services.AddMemoryCache();
        services.AddSingleton<CachedItems>();

        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();

        return services;
    }
}