using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.ApplicationServices;
using Shop.Application.AutoMapperHelper;
using Shop.Application.SignalR;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Persistence.Repositories;
using Shop.Persistence.Services;
using StackExchange.Redis;
using System.Reflection;

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

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        //services.AddScoped<IBasketRepository, BasketMemRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<UserResolverService>();
        services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfiles));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();

        services.AddSingleton<IConnectionMultiplexer>(
            _ =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

        return services;
    }
}