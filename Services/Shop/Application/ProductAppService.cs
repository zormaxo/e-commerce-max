using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos;

namespace Application;

public class ProductAppService : ProductBaseService<ShowcaseDto>
{
    public ProductAppService(IGenericRepository<Product> productsRepo,
       CachedItems cachedItems,
       IMapper mapper) : base(productsRepo, cachedItems, mapper)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }

    protected override async Task<List<ShowcaseDto>> QueryDatabase()
    {
        return await PagedAndFilteredProducts.ProjectTo<ShowcaseDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }
}