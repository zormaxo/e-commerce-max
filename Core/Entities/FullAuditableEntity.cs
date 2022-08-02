namespace Core.Entities
{
    public class FullAuditableEntity : BaseAuditableEntity
    {
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}