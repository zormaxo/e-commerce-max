using Core.Entities;

namespace Core.Interfaces
{
  public interface IGenericRepository<TEntity> where TEntity : BaseEntity
  {
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

  }
}
