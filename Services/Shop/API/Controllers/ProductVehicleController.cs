using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsVehicleController : BaseApiController
{
    private readonly ProductVehicleAppService _productMachineSrv;

    public ProductsVehicleController(ProductVehicleAppService productMachineSrv) { _productMachineSrv = productMachineSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductVehicleDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productMachineSrv.GetProducts<ProductVehicleDto>(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productMachineSrv.UpdateProduct(product); }
}