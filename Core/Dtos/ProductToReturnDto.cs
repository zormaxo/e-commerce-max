namespace Core.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public bool IsActive { get; set; }
        public bool IsNew { get; set; }
        public DateTime Created { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}