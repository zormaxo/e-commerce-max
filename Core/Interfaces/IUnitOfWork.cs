using Core.Entities;

namespace Core.Interfaces
{
  public interface IUnitOfWork :IDisposable
  {
    IProductRepository Products { get; }
    IGenericRepository<T> GenRepo<T>() where T : BaseEntity;
    Task<int> Complete();
  }
}