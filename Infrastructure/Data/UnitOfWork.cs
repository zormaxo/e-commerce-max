using Core.Entities;
using Core.Interfaces;
using Core.Repositories;
using Infrastructure.Repositories;

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

    public async Task<int> CompleteAsync()
    {
      return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}