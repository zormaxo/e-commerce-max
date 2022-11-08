namespace Shop.Core.Dtos.Product;

public class ProductMemberDto : BaseDto
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PhotoUrl { get; set; }
    public string LogoUrl { get; set; }
    public string PhoneNumber { get; set; }
    public string CreatedDate { get; set; }
    public DateTime LastActive { get; set; }
}