using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Extensions;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Shared;
using Shop.Core.Shared.Dtos;
using Shop.Core.Shared.Dtos.Product;

namespace Shop.API.Controllers;

public class AdsController : BaseApiController
{
    private readonly ProductAppService _productSrv;
    private readonly FavouritesAppService _favSrv;
    private readonly IMapper _mapper;

    public AdsController(ProductAppService productSrv, FavouritesAppService favSrv, IMapper mapper)
    {
        _productSrv = productSrv;
        _favSrv = favSrv;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Pagination<ShowcaseDto>>> GetProducts([FromQuery] ProductParams productParams)
    { return await _productSrv.GetProducts<ShowcaseDto>(productParams); }

    [HttpGet("light")]
    public async Task<ActionResult<Pagination<ProductDto>>> GetProductsLight([FromQuery] ProductParams productParams)
    { return Ok(await _productSrv.GetProducts<ProductDto>(productParams)); }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
    { return await _productSrv.GetProduct(id, User.GetUserId()); }

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

    [HttpPost("add-remove-favourite/{productId}")]
    public async Task AddRemoveFavourite(int productId) { await _favSrv.AddRemoveFavourite(productId, User.GetUserId()); }
}