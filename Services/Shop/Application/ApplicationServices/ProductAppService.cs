using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos;

namespace Shop.Application.ApplicationServices;

public class ProductAppService : ProductBaseService<ShowcaseDto>
{
    public ProductAppService(IGenericRepository<Product> productsRepo, CachedItems cachedItems, IMapper mapper) : base(
        productsRepo,
        cachedItems,
        mapper)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }

    protected override async Task<List<ShowcaseDto>> QueryDatabase()
    {
        return PagedAndFilteredProducts;
        /* return await PagedAndFilteredProducts.ProjectTo<ShowcaseDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();*/
    }
}