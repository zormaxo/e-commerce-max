using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Entities
{
    public class City : BaseEntity
    {
        [Required]
        public string CityName { get; set; }
        public ICollection<County> Counties { get; set; }
    }
}