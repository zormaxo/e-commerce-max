using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Extensions;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.Product;
using Shop.Shared.Dtos;
using Shop.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ProductAppService _productSrv;

    public ProductsController(ProductAppService productSrv) { _productSrv = productSrv; }

    [HttpGet("showcase")]
    public async Task<ActionResult<Pagination<ShowcaseDto>>> GetProductsForShowcase([FromQuery] ProductSpecParams productParams)
    { return await _productSrv.GetProducts<ShowcaseDto>(productParams); }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSrv.GetProducts<ProductDto>(productParams)); }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
    { return await _productSrv.GetProduct(id, User.GetUserId()); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> UpdateProduct(Product product) { return await _productSrv.UpdateProduct(product); }

    [HttpPost("change-active-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<int> ChangeActiveStatus(ProductActivateDto productActivateDto)
    { return await _productSrv.ChangeActiveStatus(productActivateDto); }

    [HttpGet("product-counts")]
    public async Task<ActionResult<object>> GetProductCounts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSrv.GetActiveInactiveProducts(productParams)); }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    { return await _productSrv.AddPhoto(file, User.GetUserId().Value); }

    [HttpPost("add-remove-favourite/{productId}")]
    public async Task AddRemoveFavourite(int productId) { await _productSrv.AddRemoveFavourite(productId, User.GetUserId()); }
}