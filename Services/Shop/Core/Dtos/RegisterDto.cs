using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class RegisterDto
    {
        [EmailAddress]
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }

        public string FirstName { get; set; } = "asd";
        public string Surname { get; set; } = "asd";
    }
}