namespace Shop.Shared.Dtos;

public class CategoryDto : BaseDto
{
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public ICollection<CategoryDto>? ChildCategories { get; set; }
}
