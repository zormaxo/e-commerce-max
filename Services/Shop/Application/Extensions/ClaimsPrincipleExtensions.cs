using System.Security.Claims;

namespace Shop.Application.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUserName(this ClaimsPrincipal user) { return user.FindFirst(ClaimTypes.Name)?.Value; }

    public static int? GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier);
        if (userId is null)
            return null;

        return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}