using Core.Dtos;
using Core.HelperTypes;

namespace Shop.Core.Dtos.Product
{
    public class ProductMachineDto : ShowcaseDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceText { get; set; }
        public CurrencyCode Currency { get; set; }
        public CategoryDto Category { get; set; }
        public CountyDto County { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}