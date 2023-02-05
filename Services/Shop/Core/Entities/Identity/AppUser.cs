using Microsoft.AspNetCore.Identity;

namespace Shop.Core.Entities.Identity;

public class AppUser : IdentityUser<int>
{
    public bool IsDeleted { get; set; }

    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int CreatedBy { get; set; }

    public int ModifiedBy { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public DateTime? LastActive { get; set; }

    public ICollection<Product> Products { get; set; }

    public ICollection<UserPhoto> Photos { get; set; }

    public ICollection<Favourite> Favorites { get; set; }

    public ICollection<Message> MessagesSent { get; set; }

    public ICollection<Message> MessagesReceived { get; set; }

    public ICollection<AppUserRole> UserRoles { get; set; }

    public Address Address { get; set; }
}