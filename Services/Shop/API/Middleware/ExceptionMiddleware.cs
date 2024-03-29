using Shop.API.Response;
using Shop.Core.Exceptions;
using Shop.Shared.Response;
using System.Net;
using System.Text.Json;

namespace Shop.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _env = env;
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException ex)
        {
            var response = _env.IsDevelopment()
                ? new ApiErrorObject(ex.ApiMessage, ex, (int)ex.HttpStatusCode)
                : new ApiErrorObject(ex.ApiMessage);

            await CreateExceptionResponse(ex, ex.HttpStatusCode, response);
        }
        catch (Exception ex)
        {
            var response = _env.IsDevelopment()
                ? new ApiErrorObject(nameof(ExceptionMiddleware), ex)
                : new ApiErrorObject(nameof(ExceptionMiddleware));

            await CreateExceptionResponse(ex, HttpStatusCode.InternalServerError, response);
        }

        async Task CreateExceptionResponse(Exception ex, HttpStatusCode httpStatusCode, ApiErrorObject response)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var apiResponse = ResponseWrapManager.ResponseWrapper(response, context);

            var json = JsonSerializer.Serialize(apiResponse, options);
            await context.Response.WriteAsync(json);
        }
    }
}