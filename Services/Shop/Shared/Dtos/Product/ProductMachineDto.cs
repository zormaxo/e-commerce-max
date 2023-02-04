using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.City;
using Shop.Shared.Dtos.Product;

namespace Shop.Core.Shared.Dtos.Product;

public class ProductVehicleDto : BaseProductDto
{
    public bool IsNew { get; set; }

    public string Name { get; set; }

    public string PriceText { get; set; }

    public CountyDto County { get; set; }

    public string CreatedDate { get; set; }

    public string PictureUrl { get; set; }
}