using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}