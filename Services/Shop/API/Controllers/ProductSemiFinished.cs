using Application.Specifications;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using Shop.Core.Dtos.Product;

namespace Application.Controllers
{
    public class ProductSemiFinishedController : BaseApiController
    {
        private readonly ProductMaterialAppService _productSemiFinishedSrv;

        public ProductSemiFinishedController(ProductMaterialAppService productSemiFinishedSrv)
        {
            _productSemiFinishedSrv = productSemiFinishedSrv;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductMaterialDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productSemiFinishedSrv.GetProducts(productParams));
        }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productSemiFinishedSrv.UpdateProduct(product);
        }
    }
}