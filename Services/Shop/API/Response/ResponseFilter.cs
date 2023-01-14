using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.API.Response;

public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = context.Result as ObjectResult;
        var apiResponse = ResponseWrapManager.ResponseWrapper(result.Value, context.HttpContext);
        context.Result = new OkObjectResult(apiResponse);
    }
}