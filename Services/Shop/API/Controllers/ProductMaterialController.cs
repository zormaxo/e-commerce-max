using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Helpers;
using Shop.Core.Dtos.Product;
using Shop.Core.HelperTypes;

namespace Application.Controllers
{
    public class ProductsMaterialController : BaseApiController
    {
        private readonly ProductMaterialAppService _productMaterialSrv;

        public ProductsMaterialController(ProductMaterialAppService productMaterialSrv)
        { _productMaterialSrv = productMaterialSrv; }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductMaterialDto>>> GetProducts([FromQuery] ProductParams productParams)
        { return Ok(await _productMaterialSrv.GetProducts(productParams)); }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product) { return await _productMaterialSrv.UpdateProduct(product); }
    }
}