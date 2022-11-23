using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.Application.ApplicationServices;

public class ProductAppService : ProductBaseService<ProductDto>
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
}