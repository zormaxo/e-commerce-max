using API.Errors;
using Application;
using Application.Entities;
using Application.Specifications;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using System.Net;

namespace API.Controllers
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
            return Ok(await _productSrv.GetProductsCounts(productParams));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]   //swagger documentation hints
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var productToReturnDto = await _productSrv.GetProduct(id);

            return productToReturnDto ?? (ActionResult<ProductToReturnDto>)NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productSrv.GetBrands());
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productSrv.GetCategories());
        }
    }
}