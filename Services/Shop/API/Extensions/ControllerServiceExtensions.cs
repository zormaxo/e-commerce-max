using Infrastructure;

namespace Application.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddSingleton<CachedItems>();
        services.AddScoped<ProductAppService>();
        services.AddScoped<AccountAppService>();
        services.AddScoped<UserAppService>();
        return services;
    }
}