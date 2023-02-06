using Microsoft.AspNetCore.Http.Extensions;
using Shop.Shared.Response;
using System.Net;

namespace Shop.API.Response;

public static class ResponseWrapManager
{
    public static ApiResponse ResponseWrapper(object response, HttpContext context)
    { return GenerateApiResponse(response, context); }

    public static ApiResponse ResponseWrapper(object response, HttpStatusCode statusCode)
    { return GenerateApiResponse(response, null, statusCode); }

    private static ApiResponse GenerateApiResponse(
        object response,
        HttpContext? context,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        var requestUrl = context?.Request.GetDisplayUrl() ?? string.Empty;
        var responseBody = response;
        ApiErrorObject? error = null;
        var status = true;
        var httpStatusCode = context?.Response.StatusCode ?? (int)statusCode;

        if (httpStatusCode >= 400)
        {
            status = false;
            responseBody = null;
            error = response is ApiErrorObject @object ? @object : new ApiErrorObject(response, code: httpStatusCode);
        }

        return new ApiResponse(requestUrl, responseBody, error, status, httpStatusCode);
    }
}