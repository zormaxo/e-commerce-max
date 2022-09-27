using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Infrastructure;

namespace Application;

public class ProductAppService : ProductBaseService
{
    public ProductAppService(IGenericRepository<Product> productsRepo,
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
}