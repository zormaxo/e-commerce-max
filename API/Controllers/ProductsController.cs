using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Service.BaseService.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IMapper _mapper;
    private ProductService _productSrv;


    public ProductsController(IMapper mapper, ProductService productSrv)
    {
      _productSrv = productSrv;
      _mapper = mapper;
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
