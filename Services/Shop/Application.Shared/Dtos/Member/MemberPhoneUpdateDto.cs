using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Shared.Dtos.Member;

public class MemberPhoneUpdateDto
{
    [Required]
    public string PhoneNumber { get; set; }
}