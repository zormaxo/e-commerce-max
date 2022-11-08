using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Member;

public class MemberPhoneUpdateDto
{
    [Required]
    public string PhoneNumber { get; set; }
}