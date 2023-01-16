using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Core.Response;
using Shop.Shared.Response;

namespace Shop.API.Response;

public class SampleActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        ApiResponse apiResponse;

        //if (context.Exception is not null)
        //{
        //    apiResponse = ResponseWrapManager.ResponseWrapper(
        //        new ApiErrorObject(string.Empty, context.Exception),
        //        context.HttpContext);
        //}
        if (context.Result is null)
        {
            apiResponse = ResponseWrapManager.ResponseWrapper(string.Empty, context.HttpContext);
        }
        else
        {
            var result = context.Result as ObjectResult;
            apiResponse = ResponseWrapManager.ResponseWrapper(result!.Value, context.HttpContext);
        }
        context.Result = new OkObjectResult(apiResponse);
    }
}