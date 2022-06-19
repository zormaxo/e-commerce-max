namespace Core.Entities
{
    public class AppUser : BaseAuditableEntity
    {
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}