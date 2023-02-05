using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Response;
using Shop.Shared.Response;

namespace Shop.API.Extensions;

public static class OtherExtensions
{
    public static IServiceCollection AddOtherServices(this IServiceCollection services)
    {
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
                    var apiResponse = ResponseWrapManager.ResponseWrapper(errorResponse, 404);
                    return new BadRequestObjectResult(apiResponse);
                };
            });

        return services;
    }
}