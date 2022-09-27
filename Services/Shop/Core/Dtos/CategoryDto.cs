namespace Shop.Core.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public CategoryDto Parent { get; set; }
        public string Url { get; set; }
        public bool CanBeAdded { get; set; }
        public bool MainCategory { get; set; }
        public ICollection<CategoryDto> ChildCategories { get; set; }
    }
}
