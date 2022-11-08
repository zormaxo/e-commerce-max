using Core.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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

                    return new BadRequestObjectResult(errorResponse);
                };
            });

        return services;
    }
}