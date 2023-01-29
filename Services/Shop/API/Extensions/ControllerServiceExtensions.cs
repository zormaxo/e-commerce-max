using Shop.API.Response;
using Shop.Application.ApplicationServices;
using Shop.Core.HelperTypes;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<CachedItems>();
        services.AddSingleton<SampleActionFilter>();
        services.AddScoped<AccountAppService>();
        services.AddScoped<UserAppService>();
        services.AddScoped<ProductAppService>();
        services.AddScoped<ProductMachineAppService>();
        services.AddScoped<ProductMaterialAppService>();
        services.AddScoped<CategoriesAppService>();
        services.AddScoped<BasketAppService>();


        return services;
    }
}