namespace Core.Dtos.Member;

public class MemberDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PhotoUrl { get; set; }
    public string LogoUrl { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastActive { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
}