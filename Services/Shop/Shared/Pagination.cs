namespace Shop.Shared;

public class Pagination<T> where T : class
{
    public Pagination(int pageNumber, int pageSize, List<CategoryGroupCount> categoryGroupCount, int totalCount, List<T> data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        CategoryGroupCount = categoryGroupCount;
        TotalCount = totalCount;
        Data = data;
    }

    public List<CategoryGroupCount> CategoryGroupCount { get; set; }

    public List<T> Data { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }
}