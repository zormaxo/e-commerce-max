using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Specifications
{
    public class BaseSpecifcation<T> : ISpecification<T>
    {
        public BaseSpecifcation()
        {
        }

        public BaseSpecifcation(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludesWithThen { get; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public List<string> IncludeStrings { get; } = new List<string>();

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            IncludesWithThen.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}