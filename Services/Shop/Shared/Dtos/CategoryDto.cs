namespace Shop.Shared.Dtos;

public class CategoryDto : BaseDto
{
    public string Name { get; set; }

    public string Url { get; set; }

    //public int Count { get; set; } //If this feature is deleted, it will throw an error while counting items.

    public ICollection<CategoryDto> ChildCategories { get; set; }
}
