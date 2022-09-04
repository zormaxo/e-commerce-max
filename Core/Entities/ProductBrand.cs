using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Entities
{
    public class ProductBrand : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}