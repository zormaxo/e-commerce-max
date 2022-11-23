using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Shared.Dtos;

public class RegisterDto
{
    [EmailAddress]
    [Required]
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}