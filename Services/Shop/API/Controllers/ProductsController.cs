using Application.Extensions;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Helpers;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Core.HelperTypes;

namespace Application.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly ProductAppService _productSrv;

        public ProductsController(ProductAppService productSrv) { _productSrv = productSrv; }

        [HttpGet]
        public async Task<ActionResult<Pagination<ShowcaseDto>>> GetProducts([FromQuery] ProductParams productParams)
        { return Ok(await _productSrv.GetProducts(productParams)); }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductDetailDto>> GetProduct(int id) { return await _productSrv.GetProduct(id); }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product) { return await _productSrv.UpdateProduct(product); }

        [HttpPost("change-active-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> ChangeActiveStatus(ProductActivateDto productActivateDto)
        { return await _productSrv.ChangeActiveStatus(productActivateDto); }

        [HttpGet("product-counts")]
        public async Task<ActionResult<object>> GetProductCounts([FromQuery] ProductParams productParams)
        { return Ok(await _productSrv.GetActiveInactiveProducts(productParams)); }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        { return await _productSrv.AddPhoto(file, User.GetUserId()); }
    }
}