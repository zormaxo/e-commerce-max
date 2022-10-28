using Core.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        ValueTask<T> GetByIdAsync(int id);

        Task<List<T>> ListAllAsync();

        Task AddAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<bool> SaveChangesAsync();
    }
}