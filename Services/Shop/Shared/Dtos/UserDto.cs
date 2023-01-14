namespace Shop.Shared.Dtos;

public class UserDto
{
    public int UserId { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}