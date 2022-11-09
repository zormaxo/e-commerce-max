using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Entities;

[Table("ProductPhotos")]
public class ProductPhoto : BaseEntity
{
    [Required]
    public string Url { get; set; }

    public bool IsMain { get; set; }

    public string PublicId { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }
}