using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Entities;

[Table("Favourite")]
public class Favourite : BaseEntity
{
    public int UserId { get; set; }

    public ICollection<Product> Products { get; set; }
}
