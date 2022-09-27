using Application.Extensions;
using Application.Specifications;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;

namespace Application.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly ProductAppService _productSrv;

        public ProductsController(ProductAppService productSrv)
        {
            _productSrv = productSrv;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productSrv.GetProducts(productParams));
        }

        [HttpGet("product-counts")]
        public async Task<ActionResult<object>> GetProductCounts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productSrv.GetActiveInactiveProducts(productParams));
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            return await _productSrv.AddPhoto(file, User.GetUserId());
        }
    }
}