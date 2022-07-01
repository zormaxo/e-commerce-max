using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class County : BaseEntity
    {
        public string CountyName { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}