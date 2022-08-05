using AutoMapper;
using Core;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Linq.Dynamic.Core;

namespace Service
{
    public class ProductService : BaseService
    {
        private readonly CachedItems _cachedItems;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductService(IGenericRepository<Product> productsRepo,
            IGenericRepository<Category> categoryRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            CachedItems cachedItems,
            IMapper mapper) : base(mapper)
        {
            _productsRepo = productsRepo;
            _categoryRepo = categoryRepo;
            _productBrandRepo = productBrandRepo;
            _cachedItems = cachedItems;
        }

        public async Task<Pagination<ProductToReturnDto>> GetProducts(ProductSpecParams productParams)
        {
            var filteredProducts = _productsRepo.GetAll()
                .Include(x => x.Category)
                .Include(x => x.Category.Parent)
                .Include(x => x.ProductBrand)
                .Include(x => x.Photos)
                .Include(x => x.ProductMachine)
                .Include(x => x.County).ThenInclude(x => x.City)
                .WhereIf(productParams.MaxValue.HasValue, p => p.Price < productParams.MaxValue)
                .WhereIf(productParams.MinValue.HasValue, p => p.Price > productParams.MinValue)
                .WhereIf(productParams.IsNew.HasValue, p => p.ProductMachine.IsNew == productParams.IsNew)
                .WhereIf(!string.IsNullOrEmpty(productParams.Search), p => p.Name.ToLower().Contains(productParams.Search))
                .Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(productParams.CategoryName))
            {
                List<int> categoryIds = await GetCategoryIds(productParams);
                if (categoryIds.Count > 0)
                    filteredProducts = filteredProducts.Where(x => categoryIds.Contains(x.CategoryId));

            }

            var omer = filteredProducts.GroupBy(x => x.CategoryId)
                .Select(n => new CategoryCount
                {
                    CategoryId = n.Key,
                    Count = n.Count()
                }).ToList();


            var pagedAndfilteredProducts = filteredProducts
                .OrderBy(productParams.Sort ?? "name asc")
                .PageBy(productParams);

            var totalItems = await filteredProducts.CountAsync();
            var products = await pagedAndfilteredProducts.ToListAsync();

            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, omer, data);
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
            if (_cachedItems.Categories.Count > 0)
            {
                return _cachedItems.Categories;
            }
            else
            {
                return _cachedItems.Categories = await _categoryRepo.ListAllAsync();
            }
        }

        private async Task<List<int>> GetCategoryIds(ProductSpecParams productParams)
        {
            var selectedCategory = (await GetTypes()).First(x => x.Url == productParams.CategoryName);
            List<int> categoryIds = new();
            FindChildCategories(selectedCategory);

            void FindChildCategories(Category category)
            {
                categoryIds.Add(category.Id);
                if (category.ChildCategories?.Count > 0)
                {
                    foreach (var item in category.ChildCategories)
                    {
                        FindChildCategories(item);
                    }
                }
            }

            return categoryIds;
        }
    }
}