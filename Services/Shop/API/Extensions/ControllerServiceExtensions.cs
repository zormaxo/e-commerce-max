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
using Shop.Persistence;
using Shop.Persistence.Repositories;
using Shop.Shared.Response;
using StackExchange.Redis;
using System.Net;
using System.Reflection;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment environment)
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
        //services.AddScoped<IBasketRepository, BasketMemRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfiles));

        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddHttpClient(
            "currencyfreak",
            config =>
            {
                config.BaseAddress = new Uri("https://api.currencyfreaks.com/");
            });

        services.AddSingleton(new RestClient(new HttpClient()));

        services.AddSingleton<IConnectionMultiplexer>(
            c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!);
                return ConnectionMultiplexer.Connect(options);
            });

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

        services.AddCors(
            opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4201");
                    });
            });


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

        return services;
    }
}