using API.Errors;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Helpers;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
    {
      // services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
      services.AddScoped<ProductService>();
      services.AddAutoMapper(typeof(MappingProfiles));

      AddDbContext(services, env, config);
      ConfigureValidationErrors(services);
      return services;

      static void AddDbContext(IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
      {
        Action<DbContextOptionsBuilder> optionsAction = x =>
        {
          x.UseSqlite(config.GetConnectionString("DefaultConnection"));
          if (env.IsDevelopment())
          {
            x.EnableSensitiveDataLogging();
            // x.LogTo(Console.WriteLine, LogLevel.Information);
          }
        };
        services.AddDbContext<StoreContext>(optionsAction);
      }

      static void ConfigureValidationErrors(IServiceCollection services)
      {
        services.Configure<ApiBehaviorOptions>(options =>
        {
          options.InvalidModelStateResponseFactory = actionContext =>
          {
            var errors = actionContext.ModelState
              .Where(e => e.Value.Errors.Count > 0)
              .SelectMany(x => x.Value.Errors)
              .Select(x => x.ErrorMessage).ToArray();

            var errorResponse = new ApiValidationErrorResponse
            {
              Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
          };
        });
      }
    }
  }
}
