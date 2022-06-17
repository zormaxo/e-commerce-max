namespace Core.Entities
{
    public class BaseAuditableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}