using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
  {
    protected readonly StoreContext _context;
    private DbSet<TEntity> _dbSet;
    
    public GenericRepository(StoreContext context)
    {
      _context = context;
      _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
      return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }


    public virtual void Insert(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public void Add(TEntity entity)
    {
      _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      _dbSet.AddRange(entities);
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
