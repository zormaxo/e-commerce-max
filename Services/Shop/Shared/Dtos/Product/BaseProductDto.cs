using Shop.Core.Shared.Dtos;

namespace Shop.Shared.Dtos.Product;

public class BaseProductDto : BaseDto
{
    public bool IsFavourite { get; set; }

    public int FavouriteCount => Favourites?.Count ?? 0;

    public ICollection<FavouriteDto>? Favourites { get; set; }
}
