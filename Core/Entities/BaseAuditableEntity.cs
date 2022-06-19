namespace Core.Entities
{
    public class BaseAuditableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}