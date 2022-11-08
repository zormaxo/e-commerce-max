namespace Shop.Application.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(
            int pageIndex,
            int pageSize,
            List<CategoryGroupCount> categoryGroupCount,
            int totalCount,
            IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            CategoryGroupCount = categoryGroupCount;
            TotalCount = totalCount;
            Data = data;
        }

        public List<CategoryGroupCount> CategoryGroupCount { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}