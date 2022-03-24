using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;

namespace Core.Repositories
{
  public interface IGenericRepository<T> where T : BaseEntity
  {
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
  }
}
