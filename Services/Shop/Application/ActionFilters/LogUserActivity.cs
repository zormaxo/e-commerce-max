using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Application.Extensions;
using Shop.Core.Interfaces;

namespace Shop.Application.ActionFilters;

public class LogUserActivity : IAsyncActionFilter
{
    private readonly ILoggerFactory _loggerFactory;

    public LogUserActivity(ILoggerFactory loggerFactory) { _loggerFactory = loggerFactory; }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = _loggerFactory.CreateLogger<LogUserActivity>();
        logger.LogInformation($"-----Kuyumdan Request Action: {context.ActionDescriptor.DisplayName}");

        var resultContext = await next();

        if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            return;

        var userId = resultContext.HttpContext.User.GetUserId();
        var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
        var user = await repo.GetUserByIdAsync(userId);
        user.LastActive = DateTime.UtcNow;
        await repo.SaveAllAsync();
    }
}