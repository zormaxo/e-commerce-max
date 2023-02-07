using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Shop.Application.Common.Helpers;

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

    public static IQueryable<TSource> EFBigPageBy<TSource>(this IQueryable<TSource> source, IPagedResultRequest request)
    { return source.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize); }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Major Code Smell",
        "S3358:Ternary operators should not be nested",
        Justification = "<Pending>")]
    public static IQueryable<TSource> EFBigOrderBy<TSource>(
        this IQueryable<TSource> source,
        string sort,
        CachedItems _cachedItems)
        where TSource : IPrice
    {
        if (sort == "price asc")
        {
            return source.OrderBy(
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try
                            : x.Price);
        }
        else if (sort == "price desc")
        {
            return source.OrderByDescending(
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try
                            : x.Price);
        }
        else
        {
            return source.OrderBy(sort ?? "name asc");
        }
    }
}