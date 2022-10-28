using System.Security.Claims;

namespace Shop.Application.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    { return Convert.ToInt32(user.FindFirst(ClaimTypes.NameIdentifier)?.Value); }

    public static string GetUserName(this ClaimsPrincipal user) { return user.FindFirst(ClaimTypes.Name)?.Value; }
}