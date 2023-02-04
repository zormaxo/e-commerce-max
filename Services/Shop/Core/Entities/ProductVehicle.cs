namespace Shop.Core.Entities;

public class ProductVehicle : BaseAuditableEntity
{
    public bool IsNew { get; set; }

    public Product Product { get; set; }
}