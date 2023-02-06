using System.Security.Claims;

namespace Shop.Application.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUserName(this ClaimsPrincipal user) { return user.FindFirstValue(ClaimTypes.Name) ?? string.Empty; }

    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier);
        return userId is null ? 0 : int.Parse(userId.Value);
    }

    public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
    { return user.FindFirstValue(ClaimTypes.Name) ?? string.Empty; }
}