using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Product : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public County County { get; set; }
        public int CountyId { get; set; }
        public bool Showcase { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
        public ProductMachine ProductMachine { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}