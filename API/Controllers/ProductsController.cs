using API.Controllers;
using API.Errors;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;

namespace Service.BaseService.Controllers
{
  public class ProductsController : BaseApiController
  {
    private readonly IMapper _mapper;
    private ProductService _productSrv;


    public ProductsController(IMapper mapper, ProductService productSrv)
    {
      _productSrv = productSrv;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
      return Ok(await _productSrv.GetProducts(productParams));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]   //swagger documentation hints
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var productToReturnDto = await _productSrv.GetProduct(id);

      if (productToReturnDto == null) return NotFound(new ApiResponse(404));

      return productToReturnDto;
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
