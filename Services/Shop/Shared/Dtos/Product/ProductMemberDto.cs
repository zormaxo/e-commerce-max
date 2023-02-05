namespace Shop.Shared.Dtos.Product;

public class ProductMemberDto : BaseDto
{
    public string FirstName { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string PhotoUrl { get; set; } = string.Empty;

    public string LogoUrl { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string CreatedDate { get; set; } = string.Empty;

    public DateTime LastActive { get; set; }
}