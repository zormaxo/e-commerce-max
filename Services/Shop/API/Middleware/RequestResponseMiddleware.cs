namespace Application.Middleware;

public class RequestResponseMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseMiddleware> _logger;

    public RequestResponseMiddleware(RequestDelegate next, ILogger<RequestResponseMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation($"Query Keys: {string.Join(",", httpContext.Request.QueryString)}");

        //using MemoryStream requestBody = new MemoryStream();
        //await httpContext.Request.Body.CopyToAsync(requestBody);
        //requestBody.Seek(0, SeekOrigin.Begin);
        //string requestText = await new StreamReader(requestBody).ReadToEndAsync();
        //requestBody.Seek(0, SeekOrigin.Begin);
        //_logger.LogInformation($"Request Body Mi: {requestText}");

        // Store the original body stream for restoring
        // the response body back to its original stream
        // Storing Context Body Response
        var currentBody = httpContext.Response.Body;

        // Create new memory stream for reading the response;
        // Response body streams are write-only, therefore memory stream is needed here to read
        // Using MemoryStream to hold Controller Response
        using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        // Call the next middleware
        // Passing call to Controller
        await _next(httpContext);

        // Do this last, that way you can ensure that the end results end up in the response.
        // (This resulting response may come either from the redirected route or other special routes if you have any redirection/re-execution involved in the middleware.)
        // This is very necessary. ASP.NET doesn't seem to like presenting the contents from the memory stream.
        // Therefore, the original stream provided by the ASP.NET Core engine needs to be swapped back.
        // Then write back from the previous memory stream to this original stream.
        // (The content is written in the memory stream at this point; it's just that the ASP.NET engine refuses to present the contents from the memory stream.)
        // Resetting Context Body Response
        httpContext.Response.Body = currentBody;

        // Set stream pointer position to 0 before reading
        // Setting Memory Stream Position to Beginning
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Read the body from the stream
        // Read Memory Stream data to the end
        var responseBodyText = new StreamReader(memoryStream).ReadToEnd();


        _logger.LogInformation($"Response Body Mi: {responseBodyText}");

        await httpContext.Response.WriteAsync(responseBodyText);
    }
}
