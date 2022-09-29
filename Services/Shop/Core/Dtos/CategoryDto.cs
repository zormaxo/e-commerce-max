namespace Shop.Core.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public ICollection<CategoryDto> ChildCategories { get; set; }
    }
}
