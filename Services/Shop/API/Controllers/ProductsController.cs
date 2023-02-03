using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Extensions;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.Product;
using Shop.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly ProductAppService _productSrv;

    public ProductsController(ProductAppService productSrv) { _productSrv = productSrv; }

    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDetailDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSrv.GetProducts<ProductDetailDto>(productParams)); }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ProductDetailDto> GetProduct(int id) { return _productSrv.GetProduct(id, User.GetUserId()); }

    [HttpPost("update-product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<int> UpdateProduct(Product product) { return _productSrv.UpdateProduct(product); }

    [HttpPost("change-active-status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<int> ChangeActiveStatus(ProductActivateDto productActivateDto)
    { return _productSrv.ChangeActiveStatus(productActivateDto); }

    [HttpGet("product-counts")]
    public async Task<ActionResult<object>> GetProductCounts([FromQuery] ProductSpecParams productParams)
    { return Ok(await _productSrv.GetActiveInactiveProducts(productParams)); }

    [HttpPost("add-photo")]
    public Task<PhotoDto> AddPhoto(IFormFile file) { return _productSrv.AddPhoto(file, User.GetUserId().Value); }

    [HttpPost("add-remove-favourite/{productId}")]
    public async Task AddRemoveFavourite(int productId) { await _productSrv.AddRemoveFavourite(productId, User.GetUserId()); }
}