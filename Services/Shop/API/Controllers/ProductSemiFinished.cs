using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Shared;
using Shop.Application.Shared.Dtos.Product;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;

namespace Shop.API.Controllers;

public class ProductSemiFinishedController : BaseApiController
{
    private readonly ProductMaterialAppService _productSemiFinishedSrv;

    public ProductSemiFinishedController(ProductMaterialAppService productSemiFinishedSrv)
    { _productSemiFinishedSrv = productSemiFinishedSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductMaterialDto>>> GetProducts([FromQuery] ProductParams productParams)
    { return Ok(await _productSemiFinishedSrv.GetProducts(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> UpdateProduct(Product product) { return await _productSemiFinishedSrv.UpdateProduct(product); }
}