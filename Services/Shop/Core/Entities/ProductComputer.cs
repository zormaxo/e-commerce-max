namespace Shop.Core.Entities;

public class ProductComputer : BaseAuditableEntity
{
    public bool IsNew { get; set; }

    public Product Product { get; set; }
}