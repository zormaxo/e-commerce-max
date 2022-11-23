using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Shared.Dtos.Member;

public class MemberUsernameUpdateDto
{
    [Required]
    public string Username { get; set; }
}