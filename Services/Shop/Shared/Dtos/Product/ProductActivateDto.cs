using Shop.Shared.Dtos;

namespace Shop.Shared.Dtos.Product;

public class ProductActivateDto : BaseDto
{
    public bool IsActive { get; set; }
}
