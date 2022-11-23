using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductSemiFinishedAppService : ProductBaseService<ProductDto>
{
    public ProductSemiFinishedAppService(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductMaterial> materialRepo,
        IGenericRepository<Category> categoryRepo,
        CachedItems cachedItems,
        IMapper mapper) : base(productsRepo, categoryRepo, cachedItems, mapper)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }

    protected override Task<List<ProductDto>> QueryDatabase()
    { return PagedAndFilteredProducts.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(); }
}