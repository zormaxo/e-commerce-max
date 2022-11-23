using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsMachineController : BaseApiController
{
    private readonly ProductMachineAppService _productMachineSrv;

    public ProductsMachineController(ProductMachineAppService productMachineSrv) { _productMachineSrv = productMachineSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductMachineDto>>> GetProducts([FromQuery] ProductParams productParams)
    { return Ok(await _productMachineSrv.GetProducts(productParams)); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> UpdateProduct(Product product) { return await _productMachineSrv.UpdateProduct(product); }
}