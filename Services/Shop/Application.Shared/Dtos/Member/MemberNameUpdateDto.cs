using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Member;

public class MemberNameUpdateDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}