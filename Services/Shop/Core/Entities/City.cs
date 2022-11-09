using System.ComponentModel.DataAnnotations;

namespace Shop.Core.Entities;

public class City : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public ICollection<County> Counties { get; set; }
}