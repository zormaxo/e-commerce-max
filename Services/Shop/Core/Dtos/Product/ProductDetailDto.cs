using Core.Dtos;

namespace Shop.Core.Dtos.Product
{
    public class ProductDetailDto : BaseDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PriceText { get; set; }

        public CategoryDto Category { get; set; }

        public CountyDto County { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }

        public string CreatedDate { get; set; }

        public ProductMemberDto User { get; set; }

        //For shopping
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
    }
}