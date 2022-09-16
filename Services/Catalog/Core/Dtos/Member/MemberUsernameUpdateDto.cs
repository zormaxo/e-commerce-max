using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Member;

public class MemberUsernameUpdateDto
{
    [Required]
    public string Username { get; set; }
}