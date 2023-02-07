using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shop.Persistence.Services;

public class UserResolverService
{
    private readonly IHttpContextAccessor _context;
    public UserResolverService(IHttpContextAccessor context) { _context = context; }

    public string GetUsername() { return _context?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty; }

    public int GetUserId()
    {
        int.TryParse(_context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int result);
        return result;
    }
}