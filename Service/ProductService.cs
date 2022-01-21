using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Service.Base;

namespace Service
{
  public class ProductService : BaseService
  {
    public ProductService(StoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<IReadOnlyList<ProductToReturnDto>> GetProducts()
    {
      var products = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();
      return _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
    }

    public async Task<ProductToReturnDto> GetProduct(int id)
    {
      var product = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).Where(p => p.Id == id).FirstOrDefaultAsync();
      return _mapper.Map<ProductToReturnDto>(product);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrands()
    {
      return await _context.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetTypes()
    {
      return await _context.ProductTypes.ToListAsync();
    }
  }
}