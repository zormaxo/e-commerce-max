using Microsoft.EntityFrameworkCore;
using Shop.Core.Interfaces;
using System.Linq.Expressions;

namespace Shop.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    public readonly StoreContext _context;
    public readonly DbSet<T> _dbSet;

    public GenericRepository(StoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public DbSet<T> GetAll() { return _dbSet; }

    public ValueTask<T> GetByIdAsync(int id) { return _dbSet.FindAsync(id); }

    public Task<List<T>> ListAllAsync() { return _dbSet.ToListAsync(); }

    public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) { return _dbSet.AnyAsync(predicate); }

    public async Task<bool> SaveChangesAsync() { return await _context.SaveChangesAsync() > 0; }
}