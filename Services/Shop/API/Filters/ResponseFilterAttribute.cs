using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.API.Response;
using Shop.Shared.Response;

namespace Shop.API.Filters;

public class ResponseFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        //Not needed
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        ApiResponse apiResponse;

        if (context.Result is not null)
        {
            ObjectResult result = context.Result as ObjectResult ?? new ObjectResult(string.Empty);
            apiResponse = ResponseWrapManager.ResponseWrapper(result.Value!, context.HttpContext);
        }
        else
        {
            apiResponse = ResponseWrapManager.ResponseWrapper(string.Empty, context.HttpContext);
        }

        context.Result = new OkObjectResult(apiResponse);
    }
}