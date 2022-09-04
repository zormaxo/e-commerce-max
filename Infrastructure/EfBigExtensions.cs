using Application.Interfaces;
using Core.Entities;
using Core.HelperTypes;
using Infrastructure;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Application;

public static class EFBigExtensions
{
    public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
    {
        if (condition)
            return source.Where(predicate);
        else
            return source;
    }

    public static IQueryable<TSource> EfBigPageBy<TSource>(
        this IQueryable<TSource> source, IPagedResultRequest request)
    {
        return source.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize);
    }

    public static IQueryable<TSource> EfBigOrderBy<TSource>(
    this IQueryable<TSource> source, string sort, CachedItems _cachedItems) where TSource : IPrice
    {
        //return source.Currency switch
        //{
        //    CurrencyCode.USD => decimal.Round(source.Price * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
        //    CurrencyCode.EUR => decimal.Round(source.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
        //    CurrencyCode.GBP => decimal.Round(source.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try, 2, MidpointRounding.AwayFromZero),
        //    CurrencyCode.TRY => source.Price,
        //    _ => source.Price,
        //};
        if (sort == "price asc")
        {
            source = source.OrderBy(x => x.Currency == CurrencyCode.USD ? x.Price * _cachedItems.Currency.Try
                : x.Currency == CurrencyCode.EUR ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                : x.Currency == CurrencyCode.GBP ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try : x.Price);
        }
        else if (sort == "price desc")
        {
            source = source.OrderByDescending(x => x.Currency == CurrencyCode.USD ? x.Price * _cachedItems.Currency.Try
                : x.Currency == CurrencyCode.EUR ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                : x.Currency == CurrencyCode.GBP ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try : x.Price);
        }
        else
        {
            source = source.OrderBy(sort ?? "name asc");
        }

        return source;
    }
}