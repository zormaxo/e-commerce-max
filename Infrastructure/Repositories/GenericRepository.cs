using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
  public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
  {
    private DbSet<TEntity> _dbSet;

    public GenericRepository(StoreContext context)
    {
      Context = context;
      _dbSet = context.Set<TEntity>();
    }

    protected StoreContext Context { get; private set; }

    public async Task<TEntity> GetByIdAsync(int id)
    {
      return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
      await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await _dbSet.AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
      _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
      _dbSet.RemoveRange(entities);
    }
  }
}
