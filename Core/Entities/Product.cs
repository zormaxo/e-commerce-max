namespace Core.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public County County { get; set; }
        public int CountyId { get; set; }
        public bool Showcase { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}