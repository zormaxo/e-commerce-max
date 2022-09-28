using AutoMapper;
using Core.Entities;
using Core.HelperTypes;
using Shop.Core.Dtos.Product;

namespace Application.Mapping;

public class CurrencyResolver : IValueResolver<Product, ProductDetailDto, string>
{
    public string Resolve(Product source, ProductDetailDto destination, string destMember, ResolutionContext context)
    {
        return source.Currency switch
        {
            CurrencyCode.USD => $"{source.Price} USD",
            CurrencyCode.EUR => $"{source.Price} EUR",
            CurrencyCode.GBP => $"{source.Price} GBP",
            CurrencyCode.TRY => $"{source.Price} TL",
            _ => $"{source.Price:n} TL",
        };
    }
}