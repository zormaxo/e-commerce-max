using System.Linq.Expressions;

namespace Infrastructure.Data
{
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

        public static IQueryable<TSource> PageBy<TSource>(
            this IQueryable<TSource> source, int skip, int take)
        {
            return source.Skip(skip).Take(take);
        }
    }
}