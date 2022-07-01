using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class City : BaseEntity
    {
        public string CityName { get; set; }
        public ICollection<County> Counties { get; set; }
    }
}