using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Entities;

[Table("UserPhotos")]
public class UserPhoto : BaseEntity
{
    [Required]
    public string Url { get; set; }

    public bool IsMain { get; set; }

    public string PublicId { get; set; }

    public AppUser User { get; set; }

    public int UserId { get; set; }
}