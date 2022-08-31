using System.Net;

namespace Core.Errors;

public class ApiRealException : Exception
{
    public ApiRealException(HttpStatusCode httpStatusCode, string message = null) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
    public HttpStatusCode HttpStatusCode { get; }
}