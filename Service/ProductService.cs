using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;

namespace Service
{
    public class ProductService : BaseService
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Category> _productTypeRepo;
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductService(IGenericRepository<Product> productsRepo,
            IGenericRepository<Category> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IMapper mapper) : base(mapper)
        {
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }

        public async Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams)
        {
            var filteredProducts = _productsRepo.GetAll()
                .Include(x => x.ProductType)
                .Include(x => x.ProductBrand)
                .Include(x => x.Photos)
                .Include(x => x.County).ThenInclude(x => x.City)
                .Where(x => x.IsActive);


            var pagedAndfilteredProducts = filteredProducts
                .PageBy(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);


            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        filteredProducts.OrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        filteredProducts.OrderByDescending(p => p.Price);
                        break;

                    case "dateAsc":
                        filteredProducts.OrderBy(p => p.Created);
                        break;

                    case "dateDesc":
                        filteredProducts.OrderByDescending(p => p.Created);
                        break;

                    default:
                        filteredProducts.OrderBy(n => n.Name);
                        break;
                }
            }

            //var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            //var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            //var totalItems = await _productsRepo.CountAsync(countSpec);
            //var products = await _productsRepo.ListAsync(spec);

            var totalItems = await filteredProducts.CountAsync();
            var products = await pagedAndfilteredProducts.ToListAsync();

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, data);
        }


        public async Task<object> GetProductsCounts(ProductSpecParams productParams)
        {
            var activeProducts = await _productsRepo.GetAll()
                .WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId)
                .Where(x => x.IsActive).CountAsync();

            var inactiveProducts = await _productsRepo.GetAll()
                .WhereIf(productParams.UserId.HasValue, p => p.UserId == productParams.UserId)
                .Where(x => !x.IsActive).CountAsync();

            return new { activeProducts, inactiveProducts };
        }

        public async Task<ProductToReturnDto> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<ProductToReturnDto>(product);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetBrands()
        {
            var omer = await _productBrandRepo.ListAllAsync();
            return omer;
        }

        public async Task<IReadOnlyList<Category>> GetTypes()
        {
            var omer = await _productTypeRepo.ListAllAsync();
            return omer;
        }
    }
}