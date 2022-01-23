using Core.Dtos;

namespace Core.Services
{
  public interface IProductService
  {
    public Task<IReadOnlyList<ProductToReturnDto>> GetProducts();
  }
}