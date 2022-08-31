namespace Application.Entities
{
    public class BaseAuditableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}