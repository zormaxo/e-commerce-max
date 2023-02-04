namespace Shop.Core.Entities;

public class ProductRealEstate : BaseAuditableEntity
{
    public bool IsNew { get; set; }

    public Product Product { get; set; }
}