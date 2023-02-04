using Shop.API.Filters;
using Shop.Application.ApplicationServices;
using Shop.Core.HelperTypes;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<CachedItems>();
        services.AddSingleton<ResponseFilterAttribute>();
        services.AddScoped<AccountAppService>();
        services.AddScoped<UserAppService>();
        services.AddScoped<ProductAppService>();
        services.AddScoped<ProductVehicleAppService>();
        services.AddScoped<ProductComputerAppService>();
        services.AddScoped<CategoriesAppService>();
        services.AddScoped<BasketAppService>();
        services.AddScoped<LogUserActivityAttribute>();

        return services;
    }
}