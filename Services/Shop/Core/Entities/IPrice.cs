using Shop.Core.HelperTypes;

namespace Shop.Core.Entities;

public interface IPrice
{
    public decimal Price { get; set; }

    public CurrencyCode Currency { get; set; }
}