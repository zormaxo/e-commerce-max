using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
  public class ProductRepository : GenericRepository<Product>, IProductRepository
  {
    public ProductRepository(StoreContext context) : base(context)
    {
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      return await Context.Products
          .Include(p => p.ProductType)
          .Include(p => p.ProductBrand)
          .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
      return await Context.Products
          .Include(p => p.ProductType)
          .Include(p => p.ProductBrand)
          .ToListAsync();
    }
  }
}
