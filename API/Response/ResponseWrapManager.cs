using Microsoft.AspNetCore.Http.Extensions;
using System.Net;

namespace API.Response;

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
        object error = null;
        var status = true;
        var httpStatusCode = (HttpStatusCode)context.Response.StatusCode;

        if (context.Response.StatusCode != (int)HttpStatusCode.Accepted && context.Response.StatusCode != (int)HttpStatusCode.OK)
        {
            status = false;
            responseBody = null;
            error = response;
        }

        // NOTE: Add any further customizations if needed here

        return new ApiResponse(requestUrl, responseBody, error, status, httpStatusCode);
    }
}