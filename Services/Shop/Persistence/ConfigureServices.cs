using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.Entities.Identity;
using Shop.Persistence.Services;

namespace Shop.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment environment)
    {
        services.AddScoped<StoreContextSeed>();

        var connString = string.Empty;
        if (environment.IsDevelopment())
            connString = config.GetConnectionString("DefaultConnection");
        else
        {
            // Use connection string provided at runtime by FlyIO.
            var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // Parse connection URL to connection string for Npgsql
            connUrl = connUrl!.Replace("postgres://", string.Empty);
            var pgUserPass = connUrl.Split("@")[0];
            var pgHostPortDb = connUrl.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
        }
        services.AddDbContext<StoreContext>(
            opt =>
            {
                opt.EnableSensitiveDataLogging();
                opt.UseNpgsql(connString);
            });

        services.AddIdentityCore<AppUser>(
            opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 3;
            })
            .AddRoles<AppRole>()
            .AddRoleManager<RoleManager<AppRole>>()
            .AddEntityFrameworkStores<StoreContext>();

        services.AddScoped<IStoreContext>(provider => provider.GetRequiredService<StoreContext>());

        services.AddScoped<UserResolverService>();

        return services;
    }
}