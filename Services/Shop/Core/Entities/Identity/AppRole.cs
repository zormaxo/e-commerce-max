using Microsoft.AspNetCore.Identity;

namespace Shop.Core.Entities.Identity;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}
