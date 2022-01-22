using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Service.Base;

namespace Service
{
  public class ProductService : BaseService
  {
    public ProductService(IMapper mapper, IUnitOfWork uow) : base(mapper, uow) { }

    public async Task<IReadOnlyList<ProductToReturnDto>> GetProducts()
    {
      var products = await Uow.Products.GetProductsAsync();
      // var products = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();
      return Mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
    }

    public async Task<ProductToReturnDto> GetProduct(int id)
    {
      var product = await Uow.Products.GetProductByIdAsync(id);
      // var product = await _context.Products.Include(p => p.ProductType).Include(p => p.ProductBrand).Where(p => p.Id == id).FirstOrDefaultAsync();
      return Mapper.Map<ProductToReturnDto>(product);
    }

    public async Task<IReadOnlyList<ProductBrand>> GetBrands()
    {
      return await Uow.GenRepo<ProductBrand>().GetAllAsync();
      // return await _context.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetTypes()
    {
      return await Uow.GenRepo<ProductType>().GetAllAsync();
      // return await _context.ProductTypes.ToListAsync();
    }
  }
}