using Infrastructure;
using Shop.Application.ApplicationServices;

namespace Application.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddSingleton<CachedItems>();
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