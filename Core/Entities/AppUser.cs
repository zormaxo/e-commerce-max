namespace Core.Entities
{
  public class AppUser : BaseEntity
  {
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
  }
}