using Application.Entities;
using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Core.Dtos;
using Core.Errors;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Linq.Dynamic.Core;

namespace Application;

public class ProductAppService : BaseAppService
{
    private readonly CachedItems _cachedItems;
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<Category> _categoryRepo;
    private readonly IGenericRepository<Product> _productsRepo;

    public ProductAppService(IGenericRepository<Product> productsRepo,
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
            .Include(x => x.Photos.Where(y => y.IsMain))
            .Include(x => x.ProductMachine)
            .Include(x => x.County).ThenInclude(x => x.City)
            .WhereIf(productParams.MaxValue.HasValue, p => p.Price < productParams.MaxValue)
            .WhereIf(productParams.MinValue.HasValue, p => p.Price > productParams.MinValue)
            .WhereIf(productParams.IsNew.HasValue, p => p.ProductMachine.IsNew == productParams.IsNew)
            .WhereIf(!string.IsNullOrEmpty(productParams.Search), p => p.Name.ToLower().Contains(productParams.Search))
            .Where(x => x.IsActive);

        if (!string.IsNullOrEmpty(productParams.CategoryName))
        {
            List<int> categoryIds = await GetCategoryIds(productParams.CategoryName);
            if (categoryIds.Count > 0)
                filteredProducts = filteredProducts.Where(x => categoryIds.Contains(x.CategoryId));
        }

        var pagedAndfilteredProducts = filteredProducts
            .OrderBy(productParams.Sort ?? "name asc")
            .PageBy(productParams);

        var catGrpCountList = filteredProducts.GroupBy(x => x.CategoryId)
            .Select(n => new CategoryGroupCount
            {
                CategoryId = n.Key,
                Count = n.Count()
            }).ToList();

        var totalItems = catGrpCountList.Count == 0 ?
            0 : catGrpCountList.Select(x => x.Count).Aggregate((a, b) => a + b);
        var products = await pagedAndfilteredProducts.ToListAsync();

        var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

        return new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, catGrpCountList, totalItems, data);
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
        var product = await _productsRepo.GetAll()
            .Include(x => x.Category)
            .Include(x => x.Photos)
            .Include(x => x.User)
            .Include(x => x.County).ThenInclude(x => x.City)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new ApiException(System.Net.HttpStatusCode.NotFound, $"Product with id: {id} is not found.");
        var omer = _mapper.Map<ProductToReturnDto>(product);
        return _mapper.Map<ProductToReturnDto>(product);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrands()
    {
        return await _productBrandRepo.ListAllAsync();
    }

    public async Task<IReadOnlyList<Category>> GetCategories()
    {
        if (_cachedItems.Categories.Count == 0)
            _cachedItems.Categories = await _categoryRepo.ListAllAsync();

        return _cachedItems.Categories.Where(x => x.Parent == null).ToList();
    }

    private async Task<List<int>> GetCategoryIds(string categoryName)
    {
        if (_cachedItems.Categories.Count == 0)
            _cachedItems.Categories = await _categoryRepo.ListAllAsync();

        var selectedCategory = _cachedItems.Categories.First(x => x.Url == categoryName);
        List<int> categoryIds = new();
        FindChildCategories(selectedCategory);

        void FindChildCategories(Category category)
        {
            if (category.ChildCategories?.Count > 0)
            {
                foreach (var item in category.ChildCategories)
                {
                    FindChildCategories(item);
                }
            }
            else
            {
                categoryIds.Add(category.Id);
            }
        }

        return categoryIds;
    }
}