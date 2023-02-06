namespace Shop.Core.Interfaces;

public interface IPagedResultRequest
{
    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}
