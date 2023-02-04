using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsMaterialController : BaseApiController
{
    private readonly ProductComputerAppService _productMaterialSrv;

    public ProductsMaterialController(ProductComputerAppService productMaterialSrv) { _productMaterialSrv = productMaterialSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductMaterialDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productMaterialSrv.GetProducts<ProductMaterialDto>(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productMaterialSrv.UpdateProduct(product); }
}