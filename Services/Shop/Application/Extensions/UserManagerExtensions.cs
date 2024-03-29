using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities.Identity;

namespace Shop.Application.Extensions;

public static class UserManagerExtensions
{
    public static Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> userManager, string email)
    { return userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email); }

    public static Task<AppUser> FindByEmailFromClaimsPrincipalAsync(this UserManager<AppUser> userManager, string email)
    { return userManager.Users.SingleOrDefaultAsync(x => x.Email == email); }
}