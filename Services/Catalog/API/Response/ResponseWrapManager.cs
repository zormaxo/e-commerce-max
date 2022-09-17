using Core.Errors;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json.Linq;

namespace Application.Response;

/// <summary>
/// Response Wrap Manager to handle any customizations on result and return Custom Formatted Response.
/// </summary>
public static class ResponseWrapManager
{
    /// <summary>
    /// The Response Wrapper method handles customizations and generate Formatted Response.
    /// </summary>
    /// <param name="response">The Result</param>
    /// <param name="context">The HTTP Context</param>
    /// <returns>Sample Response Object</returns>
    public static ApiResponse ResponseWrapper(object response, HttpContext context)
    {
        var requestUrl = context.Request.GetDisplayUrl();
        var responseBody = response;
        ApiErrorObject error = null;
        var status = true;
        var httpStatusCode = context.Response.StatusCode;

        if (context.Response.StatusCode >= 400)
        {
            status = false;
            responseBody = null;
            error = response is ApiErrorObject @object ? @object : ((JObject)response).ToObject<ApiErrorObject>();
        }

        // NOTE: Add any further customizations if needed here

        return new ApiResponse(requestUrl, responseBody, error, status, httpStatusCode);
    }
}