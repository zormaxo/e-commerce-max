using Core.Entities;

namespace Core.Repositories
{
  public interface IGenericRepository<TEntity> where TEntity : BaseEntity
  {
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
  }
}
