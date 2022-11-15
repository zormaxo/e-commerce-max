using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Shared.Dtos.Member;

public class MemberNameUpdateDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}