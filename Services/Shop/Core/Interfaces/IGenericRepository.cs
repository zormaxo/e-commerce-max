using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Shop.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    DbSet<T> GetAll();

    ValueTask<T> GetByIdAsync(int id);

    Task<List<T>> ListAllAsync();

    Task AddAsync(T entity);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<bool> SaveChangesAsync();

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}