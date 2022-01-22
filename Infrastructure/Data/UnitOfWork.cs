using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly StoreContext _context;
    private IProductRepository _products;

    public UnitOfWork(StoreContext context)
    {
      _context = context;
    }

    public IProductRepository Products => _products = _products ?? new ProductRepository(_context);

    public IGenericRepository<T> GenRepo<T>() where T : BaseEntity
    {
      return new GenericRepository<T>(_context);
    }

    public Task<int> Complete()
    {
      return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}