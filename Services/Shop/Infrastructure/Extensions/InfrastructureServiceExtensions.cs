using Application;
using Application.Interfaces;
using Application.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config,
        bool isProd)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserResolverService>();

        void optionsAction(DbContextOptionsBuilder x)
        {
            x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            if (!isProd)
            {
                x.EnableSensitiveDataLogging();
                //// x.LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        services.AddDbContext<StoreContext>(optionsAction);

        return services;
    }
}