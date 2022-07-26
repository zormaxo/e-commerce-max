namespace Core.Interfaces
{
    public interface IPagedResultRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
