using Application;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductSemiFinishedAppServic : ProductBaseService<ProductDto>
{
    public ProductSemiFinishedAppServic(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<Category> categoryRepo,
        IPhotoService photoService,
        CachedItems cachedItems,
        IMapper mapper,
        StoreContext context) : base(productsRepo, categoryRepo, photoService, cachedItems, mapper, context)
    {
    }

    protected override void AddCategoryFiltering()
    {
    }

    protected override async Task<List<ProductDto>> QueryDatabase()
    { return await PagedAndFilteredProducts.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(); }
}