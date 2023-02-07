using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Shop.API.Filters;
using Shop.API.Response;
using Shop.Application.ApplicationServices;
using Shop.Application.Common;
using Shop.Application.Common.Helpers.AutoMapperHelper;
using Shop.Application.Common.Interfaces.Authentication;
using Shop.Application.Common.Interfaces.Payment;
using Shop.Application.Common.Interfaces.Photo;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Photo;
using Shop.Infrastructure.Security;
using Shop.Persistence.Repositories;
using Shop.Shared.Response;
using StackExchange.Redis;
using System.Net;
using System.Reflection;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ResponseFilterAttribute>();
        services.AddSingleton<LogUserActivityAttribute>();
        services.AddSingleton<IResponseCacheService, ResponseCacheService>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPaymentService, PaymentService>();
        ////services.AddScoped<IBasketRepository, BasketMemRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfiles));

        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddHttpClient(
            "currencyfreak",
            config => config.BaseAddress = new Uri(configuration.GetValue<string>("CurrencyApi")!));

        services.AddSingleton(new RestClient(new HttpClient()));

        _ = services.AddSingleton(
            (Func<IServiceProvider, IConnectionMultiplexer>)(_ =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!);
                return ConnectionMultiplexer.Connect(options);
            }));

        services.Configure<ApiBehaviorOptions>(
            options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiErrorObject(errors);
                    var apiResponse = ResponseWrapManager.ResponseWrapper(errorResponse, HttpStatusCode.NotFound);
                    return new BadRequestObjectResult(apiResponse);
                };
            });

        services.AddHttpLogging(
            logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

        services.AddHealthChecks();

        services.AddCors(
            opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4201"));
            });

        return services;
    }
}