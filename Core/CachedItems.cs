using Core.Entities;

namespace Core
{
    public class CachedItems
    {
        public IReadOnlyList<Category> Categories { get; set; } = new List<Category>();
    }
}
