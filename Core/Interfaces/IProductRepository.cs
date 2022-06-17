using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Task<Product> GetByIdAsync(int id);

        //Task<IReadOnlyList<Product>> GetProductsAsync();

        //Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        //Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}