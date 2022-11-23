using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Shared.Dtos.Member;

public class MemberPhoneUpdateDto
{
    [Required]
    public string PhoneNumber { get; set; }
}