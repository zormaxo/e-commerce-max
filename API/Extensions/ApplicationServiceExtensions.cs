using API.Data;
using API.Errors;
using Core.Interfaces;
using Core.Repositories;
using Core.Service.Helpers;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfiles));

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

            return services;
        }
    }
}