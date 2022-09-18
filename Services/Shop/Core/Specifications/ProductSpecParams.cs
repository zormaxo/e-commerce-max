using Application.Interfaces;
using Core.HelperTypes;

namespace Application.Specifications;

public class ProductSpecParams : IPagedResultRequest
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int? BrandId { get; set; }
    public string CategoryName { get; set; }
    public string Sort { get; set; }
    private string _search;

    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }

    public bool? IsNew { get; set; }
    public int? MinValue { get; set; }
    public int? MaxValue { get; set; }
    public int? CityId { get; set; }
    public int? CountyId { get; set; }
    public bool? GetAllStatus { get; set; }
    public int? UserId { get; set; }
    public CurrencyCode Currency { get; set; }
}