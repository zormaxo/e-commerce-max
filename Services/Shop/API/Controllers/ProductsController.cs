using Application.Extensions;
using Application.Specifications;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;

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
        public async Task<ActionResult<Pagination<ShowcaseDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productSrv.GetProducts(productParams));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
        {
            return await _productSrv.GetProduct(id);
        }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productSrv.UpdateProduct(product);
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