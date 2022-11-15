using Shop.Application.Shared.Dtos.City;
using Shop.Application.Shared.Dtos.Member;

namespace Shop.Application.Shared.Dtos.Product;

public class ProductToReturnDto : ShowcaseDto
{
    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PriceText { get; set; }

    public CategoryDto Category { get; set; }

    public CountyDto County { get; set; }

    public bool IsActive { get; set; }

    public ICollection<PhotoDto> Photos { get; set; }

    //public ProductMachine ProductMachine { get; set; }
    //public ProductMaterial ProductMaterial { get; set; }
    public DateTime CreatedDate { get; set; }

    public MemberDto User { get; set; }
    //public Category Category { get; set; }
    //public CountyDto County { get; set; }


    //public string ProductType { get; set; }
}