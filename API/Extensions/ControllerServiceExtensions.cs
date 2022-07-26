using Core;
using Service;

namespace API.Extensions
{
    public static class ControllerServiceExtensions
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddSingleton<CachedItems>();
            services.AddScoped<ProductService>();
            services.AddScoped<AccountService>();
            services.AddScoped<UserService>();
            return services;
        }
    }
}