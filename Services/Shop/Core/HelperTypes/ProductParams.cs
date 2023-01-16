namespace Shop.Core.HelperTypes;

public class ProductSpecParams : PaginationParams
{
    private string? _search;

    public int? BrandId { get; set; }

    public string? CategoryName { get; set; }

    public int? CityId { get; set; }

    public int? CountyId { get; set; }

    public CurrencyCode Currency { get; set; }

    public bool? GetAllStatus { get; set; }

    public bool? IsNew { get; set; }

    public int? MaxValue { get; set; }

    public int? MinValue { get; set; }

    public string? Search { get => _search; set => _search = value.ToLower(); }

    public string? Sort { get; set; }

    public int? UserId { get; set; }

    public bool? Favourite { get; set; }
}