using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Category : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public int Count { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public string Url { get; set; }
    public bool CanBeAdded { get; set; }
    public bool MainCategory { get; set; }
    public ICollection<Category> ChildCategories { get; set; }
}