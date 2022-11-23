using Shop.Core.Shared.Dtos.City;

namespace Shop.Core.Shared.Dtos.Product;

public class ProductMaterialDto : BaseDto
{
    public string Name { get; set; }

    public string PriceText { get; set; }

    public CountyDto County { get; set; }

    public string CreatedDate { get; set; }

    public string PictureUrl { get; set; }
}