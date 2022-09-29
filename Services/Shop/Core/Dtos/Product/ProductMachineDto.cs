using Core.Dtos;
using Core.HelperTypes;

namespace Shop.Core.Dtos.Product
{
    public class ProductMachineDto : BaseDto
    {
        public string Name { get; set; }
        public string PriceText { get; set; }
        public CountyDto County { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public string CreatedDate { get; set; }
    }
}