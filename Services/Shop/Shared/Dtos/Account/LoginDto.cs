using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Account;

public class LoginDto
{
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(4, ErrorMessage = "Password must be a minimum length of '4'")]
    public string Password { get; set; } = string.Empty;
}