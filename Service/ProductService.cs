using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Service.Helpers;

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

        public async Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams)
        {
          var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data);
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