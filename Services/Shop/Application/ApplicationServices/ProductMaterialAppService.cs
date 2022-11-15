using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Shared.Dtos.Product;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Persistence;

namespace Shop.Application.ApplicationServices;

public class ProductMaterialAppService : ProductBaseService<ProductMaterialDto>
{
    IGenericRepository<ProductMaterial> _materialRepo;

    public ProductMaterialAppService(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductMaterial> materialRepo,
        IGenericRepository<Category> categoryRepo,
        CachedItems cachedItems,
        IMapper mapper) : base(productsRepo, categoryRepo, cachedItems, mapper)
    { _materialRepo = materialRepo; }

    protected override void AddCategoryFiltering()
    {
        var categoryFilter = _materialRepo.GetAll().WhereIf(ProductParams.IsNew.HasValue, p => p.IsNew == ProductParams.IsNew);

        FilteredProducts = FilteredProducts.Where(x => categoryFilter.Any(y => y.Id == x.Id));
    }

    protected async override Task<List<ProductMaterialDto>> QueryDatabase()
    {
        return await PagedAndFilteredProducts.ProjectTo<ProductMaterialDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }
}