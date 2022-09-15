using Core.Entities;

namespace Application.Entities
{
    public class FullAuditableEntity : BaseAuditableEntity
    {
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}