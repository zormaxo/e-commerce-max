using Core.Errors;
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
    /// <param name="result">The Result</param>
    /// <param name="context">The HTTP Context</param>
    /// <param name="exception">The Exception</param>
    /// <returns>Sample Response Object</returns>
    public static SampleResponse ResponseWrapper(object result, HttpContext context)
    {
        var requestUrl = context.Request.GetDisplayUrl();
        var data = result;
        object error = null;
        var status = true;
        var httpStatusCode = (HttpStatusCode)context.Response.StatusCode;

        if (context.Response.StatusCode != (int)HttpStatusCode.Accepted && context.Response.StatusCode != (int)HttpStatusCode.OK)
        {
            status = false;
            data = null;
            error = result;
        }

        // NOTE: Add any further customizations if needed here

        return new SampleResponse(requestUrl, data, error, status, httpStatusCode);
    }
}