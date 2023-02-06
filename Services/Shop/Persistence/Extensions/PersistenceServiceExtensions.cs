using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Persistence.Services;
using StackExchange.Redis;

namespace Shop.Persistence.Extensions;

public static class PersistenceServiceExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config, bool isProd)
    {
        services.AddSingleton<IConnectionMultiplexer>(
            _ =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });

        services.AddScoped<UserResolverService>();


        //void optionsAction(DbContextOptionsBuilder x)
        //{
        //    x.UseSqlite(config.GetConnectionString("DefaultConnection"));
        //    if (!isProd)
        //    {
        //        x.EnableSensitiveDataLogging();
        //        //// x.LogTo(Console.WriteLine, LogLevel.Information);
        //    }

        //    //This supress Entity 'Product' has a global query filter defined and is the required end of a relationship with the entity 'Favourite'.
        //    x.ConfigureWarnings(
        //        builder =>
        //        {
        //            builder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning);
        //        });
        //}

        //services.AddDbContext<StoreContext>(optionsAction);

        //services.AddDbContext<StoreContext>(
        //    opt =>
        //    {
        //        opt.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        //    });

        return services;
    }
}