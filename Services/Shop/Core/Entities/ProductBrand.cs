using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Entities;

public class ProductBrand : BaseEntity
{
    [Required]
    public string Name { get; set; }
}