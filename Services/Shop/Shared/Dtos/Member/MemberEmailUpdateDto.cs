using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Member;

public class MemberEmailUpdateDto
{
    [Required]
    public string Email { get; set; } = string.Empty;
}