using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class AppUser : BaseAuditableEntity
    {
        [Required]
        public string UserName { get; set; }
        public string Logo { get; set; }
        public string PhotoUrl { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}