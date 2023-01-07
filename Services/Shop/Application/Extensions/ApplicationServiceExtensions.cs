using System.Reflection;
using API.SignalR;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.ActionFilters;
using Shop.Application.AutoMapperHelper;
using Shop.Core.Interfaces;
using Shop.Persistence.Repositories;
using Shop.Persistence.Services;
using StackExchange.Redis;

namespace Shop.Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<LogUserActivity>();
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