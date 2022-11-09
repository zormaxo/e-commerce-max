namespace Shop.Application.Shared.Dtos.Member;

public class MemberDto : BaseDto
{
    public MemberDto()
    {
    }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhotoUrl { get; set; }

    public string LogoUrl { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastActive { get; set; }

    public ICollection<PhotoDto> Photos { get; set; }
}