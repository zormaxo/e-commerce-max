using Application.Entities;
using AutoMapper;
using Core.Dtos;
using Core.HelperTypes;
using Infrastructure;

namespace Application.Mapping;

public class CurrencyResolver : IValueResolver<Product, ProductToReturnDto, decimal>
{
    private readonly CachedItems _cachedItems;

    public CurrencyResolver(CachedItems cachedItems)
    {
        _cachedItems = cachedItems;
    }

    public decimal Resolve(Product source, ProductToReturnDto destination, decimal destMember, ResolutionContext context)
    {
        return source.Currency switch
        {
            CurrencyCode.USD => decimal.Round(source.Price * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            CurrencyCode.EUR => decimal.Round(source.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            CurrencyCode.GBP => decimal.Round(source.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
            CurrencyCode.TRY => source.Price,
            _ => source.Price,
        };
    }
}