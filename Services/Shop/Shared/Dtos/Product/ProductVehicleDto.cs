using Shop.Shared.Dtos.City;

namespace Shop.Shared.Dtos.Product;

public class ProductVehicleDto : BaseProductDto
{
    public bool IsNew { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PriceText { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public CountyDto County { get; set; } = null!;
}