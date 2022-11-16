using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Entities;

[Table("Favourites")]
public class Favourite
{
    public AppUser User { get; set; }

    public int UserId { get; set; }

    public Product LikedProduct { get; set; }

    public int LikedProductId { get; set; }
}
