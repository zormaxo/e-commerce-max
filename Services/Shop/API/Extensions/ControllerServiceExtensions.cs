using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Filters;
using Shop.API.Response;
using Shop.Application.ApplicationServices;
using Shop.Application.Common.Helpers.AutoMapperHelper;
using Shop.Application.Common.Interfaces.Authentication;
using Shop.Application.Common.Interfaces.Photo;
using Shop.Core.Interfaces;
using Shop.Infrastructure.Photo;
using Shop.Infrastructure.Security;
using Shop.Persistence.Repositories;
using Shop.Shared.Response;
using System.Net;
using System.Reflection;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddSingleton<ResponseFilterAttribute>();
        services.AddSingleton<LogUserActivityAttribute>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IBasketRepository, BasketMemRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(MappingProfiles));

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

        return services;
    }
}