using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Shared.Dtos;

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Mvc, 5 karakterden uzun olamaz.")]
    public string Password { get; set; }
}