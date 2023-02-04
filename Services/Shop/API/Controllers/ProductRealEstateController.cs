using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductRealEstateController : BaseApiController
{
    private readonly ProductRealEstateAppService _productSemiFinishedSrv;

    public ProductRealEstateController(ProductRealEstateAppService productSemiFinishedSrv)
    { _productSemiFinishedSrv = productSemiFinishedSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductComputerDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSemiFinishedSrv.GetProducts<ProductComputerDto>(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productSemiFinishedSrv.UpdateProduct(product); }
}