using Shop.Core.Shared.Dtos;

namespace Shop.Shared.Dtos.Member;

public class MemberDto : BaseDto
{
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhotoUrl { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime LastActive { get; set; }

    public ICollection<PhotoDto>? Photos { get; set; }
}