using Core.Entities;

namespace Application.Entities
{
    public class ProductMaterial : BaseAuditableEntity
    {
        public bool IsNew { get; set; }
        public Product Product { get; set; }
    }
}