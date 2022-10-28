using Application.Helpers;
using Application.Interfaces;
using Application.Mapping;
using Application.Repositories;
using Application.Services;
using Core.Errors;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Helpers;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Repositories;
using StackExchange.Redis;

namespace Application.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IWebHostEnvironment env,
        IConfiguration config)
    {
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<UserResolverService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfiles));

        services.Configure<ApiBehaviorOptions>(
            options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiErrorObject(errors);

                    return new BadRequestObjectResult(errorResponse);
                };
            });

        void optionsAction(DbContextOptionsBuilder x)
        {
            x.UseSqlite(config.GetConnectionString("DefaultConnection"));
            if (env.IsDevelopment())
            {
                x.EnableSensitiveDataLogging();
                //// x.LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        services.AddDbContext<StoreContext>(optionsAction);
        services.AddSingleton<IConnectionMultiplexer>(
            _ =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

        return services;
    }
}