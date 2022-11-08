using Core.HelperTypes;

namespace Core.Entities;

public interface IPrice
{
    public decimal Price { get; set; }
    public CurrencyCode Currency { get; set; }
}