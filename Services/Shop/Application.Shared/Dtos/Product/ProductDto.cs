using Shop.Application.Shared.Dtos.City;

namespace Shop.Application.Shared.Dtos.Product;

public class ProductDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;

    //public CategoryDto Category { get; set; }
    public int CategoryId { get; set; }

    public CountyDto County { get; set; }

    public ICollection<PhotoDto> Photos { get; set; }

    public string CreatedDate { get; set; } = string.Empty;

    public ProductMemberDto User { get; set; }

    //For shopping
    public string PictureUrl { get; set; } = string.Empty;

    //public decimal Price { get; set; }

    public bool IsFavourite { get; set; }

    public int FavouriteCount { get; set; }

    public ICollection<FavouriteDto> Favourites { get; set; }
}