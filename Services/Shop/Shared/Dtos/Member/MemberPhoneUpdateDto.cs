using System.ComponentModel.DataAnnotations;

namespace Shop.Shared.Dtos.Member;

public class MemberPhoneUpdateDto
{
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
}