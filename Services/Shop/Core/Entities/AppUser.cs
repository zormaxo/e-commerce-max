using Application.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AppUser : FullAuditableEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string LogoUrl { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<UserPhoto> Photos { get; set; }
    }
}