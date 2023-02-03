using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Extensions;
using Shop.Core.Interfaces;

namespace Shop.API.Filters;

public class LogUserActivityAttribute : IAsyncActionFilter
{
    private readonly ILoggerFactory _loggerFactory;

    public LogUserActivityAttribute(ILoggerFactory loggerFactory) { _loggerFactory = loggerFactory; }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = _loggerFactory.CreateLogger<LogUserActivityAttribute>();
        logger.LogInformation($"-----Kuyumdan Request Action: {context.ActionDescriptor.DisplayName}");

        var resultContext = await next();

        if (resultContext?.HttpContext?.User?.Identity is null)
            return;

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            return;

        var userId = resultContext.HttpContext.User.GetUserId();

        if (userId is not null)
        {
            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repo!.GetUserByIdAsync(userId.Value);
            user.LastActive = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}