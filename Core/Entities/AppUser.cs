using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class AppUser : FullAuditableEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
        public string LogoUrl { get; set; }
        public string PhotoUrl { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}