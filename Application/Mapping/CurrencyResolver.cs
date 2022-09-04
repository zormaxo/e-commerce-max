using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.HelperTypes;
using Infrastructure;

namespace Application.Mapping;

public class CurrencyResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly CachedItems _cachedItems;

    public CurrencyResolver(CachedItems cachedItems)
    {
        _cachedItems = cachedItems;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        return source.Currency switch
        {
            CurrencyCode.USD => $"{source.Price} USD",
            CurrencyCode.EUR => $"{source.Price} EUR",
            CurrencyCode.GBP => $"{source.Price} GBP",
            CurrencyCode.TRY => $"{source.Price} TL",
            _ => $"{source.Price:n} TL",

            //CurrencyCode.USD => $"{source.Price * _cachedItems.Currency.Try:n} USD",
            //CurrencyCode.EUR => $"{source.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try:n} EUR",
            //CurrencyCode.GBP => $"{source.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try:n} GBP",
            //CurrencyCode.TRY => $"{source.Price:n} TL",
            //_ => $"{source.Price:n} TL",

            //CurrencyCode.USD => decimal.Round(source.Price * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            //CurrencyCode.EUR => decimal.Round(source.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            //CurrencyCode.GBP => decimal.Round(source.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            //CurrencyCode.TRY => source.Price,
            //_ => source.Price,
        };
    }
}