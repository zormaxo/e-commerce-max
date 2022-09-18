using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services;

public class UserResolverService
{
    private readonly IHttpContextAccessor _context;
    public UserResolverService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string GetUser()
    {
        return _context.HttpContext.User?.Identity?.Name;
    }
    public int GetUserId()
    {
        return Convert.ToInt32(_context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }
}