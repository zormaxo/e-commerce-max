using Application.Specifications;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using Shop.Core.Dtos.Product;

namespace Application.Controllers
{
    public class ProductsMachineController : BaseApiController
    {
        private readonly ProductMachineAppService _productMachineSrv;

        public ProductsMachineController(ProductMachineAppService productMachineSrv)
        {
            _productMachineSrv = productMachineSrv;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductMachineDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productMachineSrv.GetProducts(productParams));
        }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productMachineSrv.UpdateProduct(product);
        }
    }
}