using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    public readonly StoreContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(StoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll() { return _dbSet; }

    public ValueTask<T> GetByIdAsync(int id) { return _dbSet.FindAsync(id); }

    public Task<List<T>> ListAllAsync() { return _dbSet.ToListAsync(); }

    public async Task AddAsync(T entity) { await _dbSet.AddAsync(entity); }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) { return _dbSet.AnyAsync(predicate); }

    public async Task<bool> SaveChangesAsync() { return await _context.SaveChangesAsync() > 0; }
}