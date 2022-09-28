using Core.Dtos;
using Core.HelperTypes;
using Shop.Core.Extensions;

namespace Shop.Core.Dtos.Product
{
    public class ProductDetailDto
    {
        private decimal price;

        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price
        {
            get => price;
            set
            {
                price = value;
                PriceText = value.ToString().ToPriceText(Currency);
            }
        }

        public string PriceText { get; set; }
        public CurrencyCode Currency { get; set; }
        public CategoryDto Category { get; set; }
        public CountyDto County { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductMemberDto User { get; set; }
    }
}