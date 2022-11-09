using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Shared.Dtos;

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MaxLength(5, ErrorMessage = "Mvc, 5 karakterden uzun olamaz.")]
    public string Password { get; set; }
}