using System.Net;

namespace Shop.Core.Exceptions;

public class ApiException : Exception
{
    public ApiException(string apiMessage = null)
    {
        HttpStatusCode = HttpStatusCode.BadRequest;
        ApiMessage = apiMessage ?? GetDefaultMessageForStatusCode(HttpStatusCode.BadRequest);
    }

    public ApiException(HttpStatusCode httpStatusCode, object apiMessage = null)
    {
        HttpStatusCode = httpStatusCode;
        ApiMessage = apiMessage ?? GetDefaultMessageForStatusCode(httpStatusCode);
    }

    public HttpStatusCode HttpStatusCode { get; }

    public object ApiMessage { get; }

    private static string GetDefaultMessageForStatusCode(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.BadRequest => "A bad request, you have made",
            HttpStatusCode.Unauthorized => "Authorized, you are not",
            HttpStatusCode.NotFound => "Resource found, it was not",
            HttpStatusCode.InternalServerError => "Errors are the path to the dark side.  Errors lead to anger.   Anger leads to hate.  Hate leads to career change.",
            _ => null
        };
    }
}