using System.Net;
using API.Errors;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Helpers;

namespace API.Controllers
{
  public class ProductsController : BaseApiController
  {
    private ProductService _productSrv;

    public ProductsController(ProductService productSrv)
    {
      _productSrv = productSrv;
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

      if (productToReturnDto == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

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
