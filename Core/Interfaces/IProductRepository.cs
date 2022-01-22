using Core.Entities;

namespace Core.Interfaces
{
  public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}