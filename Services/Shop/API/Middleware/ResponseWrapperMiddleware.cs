using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shop.API.Response;
using Shop.Shared.Response;
using System.Net;

namespace Shop.API.Middleware;

/// <summary>
/// Response Wrapper Middleware to Request Delegate and handles Request/Response Customizations.
/// </summary>
public class ResponseWrapperMiddleware
{
    /// <summary>
    /// Request Delegate field to invoke HTTP Context
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// The Response Wrapper Middleware Constructor
    /// </summary>
    /// <param name="next">The Request Delegate</param>
    public ResponseWrapperMiddleware(RequestDelegate next) => _next = next;

    /// <summary>
    /// Invoke Method for the HttpContext
    /// </summary>
    /// <param name="context">The HTTP Context</param>
    /// <returns>Response</returns>
    public async Task Invoke(HttpContext context)
    {
        // Store the original body stream for restoring
        // the response body back to its original stream
        // Storing Context Body Response
        var currentBody = context.Response.Body;

        // Create new memory stream for reading the response;
        // Response body streams are write-only, therefore memory stream is needed here to read
        // Using MemoryStream to hold Controller Response
        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        // Call the next middleware
        // Passing call to Controller
        await _next(context);

        // Do this last, that way you can ensure that the end results end up in the response.
        // (This resulting response may come either from the redirected route or other special routes if you have any redirection/re-execution involved in the middleware.)
        // This is very necessary. ASP.NET doesn't seem to like presenting the contents from the memory stream.
        // Therefore, the original stream provided by the ASP.NET Core engine needs to be swapped back.
        // Then write back from the previous memory stream to this original stream.
        // (The content is written in the memory stream at this point; it's just that the ASP.NET engine refuses to present the contents from the memory stream.)
        // Resetting Context Body Response
        context.Response.Body = currentBody;

        // Set stream pointer position to 0 before reading
        // Setting Memory Stream Position to Beginning
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Read the body from the stream
        // Read Memory Stream data to the end
        var responseBodyText = new StreamReader(memoryStream).ReadToEnd();

        if (context.Request.GetDisplayUrl().Contains("hubs"))
        {
            await context.Response.WriteAsync(responseBodyText);
            return;
        }

        // Deserializing Controller Response to an object
        var responseObj = new object();
        if (context.Response.StatusCode == (int)HttpStatusCode.NoContent)
        {
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentLength = null;
        }

        try
        {
            responseObj = JsonConvert.DeserializeObject(responseBodyText);
        }
        catch
        {
            if (context.Response.StatusCode != (int)HttpStatusCode.Accepted &&
                context.Response.StatusCode != (int)HttpStatusCode.OK)
            {
                responseObj = new ApiErrorObject(responseBodyText);
            }
            else
            {
                responseObj = responseBodyText;
            }
        }

        // Invoking Customizations Method to handle Custom Formatted Response
        var response = ResponseWrapManager.ResponseWrapper(responseObj, context);

        context.Response.ContentType = "application/json";

        // returing response to caller
        await context.Response
            .WriteAsync(
                JsonConvert.SerializeObject(
                    response,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
    }
}

//https://stackoverflow.com/questions/43403941/how-to-read-asp-net-core-response-body
//https://stackoverflow.com/questions/72385997/net-core-api-modify-response-globally
