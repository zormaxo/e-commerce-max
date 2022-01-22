using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Base;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private ProductService _productSrv;

    public ProductsController(FwServiceCollection srvCollection)
    {
      _productSrv = srvCollection.ProductSrv;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
      return Ok(await _productSrv.GetProducts());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      return await _productSrv.GetProduct(id);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
      return Ok(await _productSrv.GetBrands());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
    {
      return Ok(await _productSrv.GetTypes());
    }
  }
}
