using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Member;

public class MemberNameUpdateDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}