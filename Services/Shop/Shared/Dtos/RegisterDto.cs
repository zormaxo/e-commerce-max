using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}