using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductSemiFinishedController : BaseApiController
{
    private readonly ProductRealEstateAppService _productSemiFinishedSrv;

    public ProductSemiFinishedController(ProductRealEstateAppService productSemiFinishedSrv)
    { _productSemiFinishedSrv = productSemiFinishedSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductMaterialDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSemiFinishedSrv.GetProducts<ProductMaterialDto>(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productSemiFinishedSrv.UpdateProduct(product); }
}