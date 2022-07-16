using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public int Count { get; set; }
        public int? ParentId { get; set; }
        public CategoryDto Parent { get; set; }
        //public ICollection<CategoryDto> Categories { get; set; }

    }
}