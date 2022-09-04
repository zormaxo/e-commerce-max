using Application.Interfaces;
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

    public static IOrderedQueryable<TSource> EfBigOrderBy<TSource>(
    this IQueryable<TSource> source, string sort)
    {
        return source.OrderBy(sort ?? "name asc");
    }
}