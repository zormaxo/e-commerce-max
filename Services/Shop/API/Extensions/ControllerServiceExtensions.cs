using Microsoft.AspNetCore.Mvc;
using Shop.API.Filters;
using Shop.API.Response;
using Shop.Shared.Response;
using System.Net;

namespace Shop.API.Extensions;

public static class ControllerServiceExtensions
{
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
        services.AddSingleton<ResponseFilterAttribute>();
        services.AddSingleton<LogUserActivityAttribute>();

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