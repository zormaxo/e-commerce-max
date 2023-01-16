namespace Shop.Core.Entities;

public class FullAuditableEntity : BaseAuditableEntity
{
    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int CreatedBy { get; set; }

    public int ModifiedBy { get; set; }
}