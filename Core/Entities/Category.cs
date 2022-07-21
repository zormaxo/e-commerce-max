using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int Count { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string Url { get; set; }
        public ICollection<Category> ChildCategories { get; set; }

    }
}