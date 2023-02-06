using Shop.Core.Interfaces;

namespace Shop.Core.HelperTypes;

public class PaginationParams : IPagedResultRequest
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize { get => _pageSize; set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
}
