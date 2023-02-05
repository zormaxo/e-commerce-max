using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Shared;
using Shop.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsComputerController : BaseApiController
{
    private readonly ProductComputerAppService _productMaterialSrv;

    public ProductsComputerController(ProductComputerAppService productMaterialSrv) { _productMaterialSrv = productMaterialSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductComputerDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productMaterialSrv.GetProducts<ProductComputerDto>(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productMaterialSrv.UpdateProduct(product); }
}