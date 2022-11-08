﻿using Application.Interfaces;
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
        if(condition)
            return source.Where(predicate);
        else
            return source;
    }

    public static IQueryable<TSource> EFBigPageBy<TSource>(this IQueryable<TSource> source, IPagedResultRequest request)
    { return source.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize); }

    public static IQueryable<TSource> EFBigOrderBy<TSource>(
        this IQueryable<TSource> source,
        string sort,
        CachedItems _cachedItems)
        where TSource : IPrice
    {
        if(sort == "price asc")
        {
            return source.OrderBy(
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try
                            : x.Price);
        } else if(sort == "price desc")
        {
            return source.OrderByDescending(
                x => x.Currency == CurrencyCode.USD
                    ? x.Price * _cachedItems.Currency.Try
                    : x.Currency == CurrencyCode.EUR
                        ? x.Price / _cachedItems.Currency.Eur * _cachedItems.Currency.Try
                        : x.Currency == CurrencyCode.GBP
                            ? x.Price / _cachedItems.Currency.Gbp * _cachedItems.Currency.Try
                            : x.Price);
        } else
        {
            return source.OrderBy(sort ?? "name asc");
        }
    }
}