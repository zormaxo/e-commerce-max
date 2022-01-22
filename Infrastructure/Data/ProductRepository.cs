using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class ProductRepository : GenericRepository<Product>, IProductRepository
  {
    public ProductRepository(StoreContext context) :base(context)
    {
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      return await _context.Products
          .Include(p => p.ProductType)
          .Include(p => p.ProductBrand)
          .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
      return await _context.Products
          .Include(p => p.ProductType)
          .Include(p => p.ProductBrand)
          .ToListAsync();
    }
  }
}
