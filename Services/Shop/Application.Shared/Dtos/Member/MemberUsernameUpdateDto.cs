using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Shared.Dtos.Member;

public class MemberUsernameUpdateDto
{
    [Required]
    public string Username { get; set; }
}