using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    [Table("Photos")]
    public class Photo : BaseEntity
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}