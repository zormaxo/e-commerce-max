namespace Shop.Core.Shared.Dtos.Member;

public class MemberLightDto : BaseDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhotoUrl { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}