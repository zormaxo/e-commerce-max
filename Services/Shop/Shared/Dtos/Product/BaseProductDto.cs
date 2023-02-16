namespace Shop.Shared.Dtos.Product;

public class BaseProductDto : BaseDto
{
    //For shopping
    public string PictureUrl { get; set; } = string.Empty;

    public bool IsFavourite { get; set; }

    public int FavouriteCount => Favourites?.Count ?? 0;

    public ICollection<PhotoDto>? Photos { get; set; }

    public ICollection<FavouriteDto>? Favourites { get; set; }
}
