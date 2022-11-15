using Microsoft.AspNetCore.Mvc;
using Shop.Core.Response;
using System.Net;

namespace Shop.API.Controllers;

[Route("errors/{code}")]
[ApiExplorerSettings(IgnoreApi = true)]     //we dont want swagger to add this.
public class ErrorController : BaseApiController
{
    public ActionResult<ApiErrorObject> Error(int code)
    {
        string message = GetDefaultMessageForStatusCode((HttpStatusCode)code);
        return new ObjectResult($"{message}: {code}");
    }

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