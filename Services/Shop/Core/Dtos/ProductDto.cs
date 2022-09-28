using Application.Entities;
using Core.Dtos;
using Core.Dtos.Member;
using Core.HelperTypes;

namespace Shop.Core.Dtos
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; }
        public MemberDto User { get; set; }
        public CountyDto County { get; set; }
        public bool Showcase { get; set; }
        public bool IsActive { get; set; }
        public CurrencyCode Currency { get; set; }
        public ProductMachine ProductMachine { get; set; }
        public ProductMaterial ProductMaterial { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
