using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Repositories;
using Core.Specifications;
using Service.Base;

namespace Service
{
  public class ProductService : BaseService
  {
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IGenericRepository<Product> _productsRepo;

    public ProductService(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IMapper mapper) : base(mapper)
    {
      _productsRepo = productsRepo;
      _productTypeRepo = productTypeRepo;
      _productBrandRepo = productBrandRepo;
    }

    public async Task<IReadOnlyList<ProductToReturnDto>> GetProducts()
    {
      var spec = new ProductsWithTypesAndBrandsSpecification();
      var products = await _productsRepo.ListAsync(spec);

      return _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
    }

    public async Task<ProductToReturnDto> GetProduct(int id)
    {
      var spec = new ProductsWithTypesAndBrandsSpecification(id);
      var product = await _productsRepo.GetEntityWithSpec(spec);

      return _mapper.Map<ProductToReturnDto>(product);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrands()
    {
      return await _productBrandRepo.ListAllAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetTypes()
    {
      return await _productTypeRepo.ListAllAsync();
    }
  }
}