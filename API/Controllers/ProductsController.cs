using Application;
using Application.Entities;
using Application.Specifications;
using Core.Dtos;
using Core.Entities;
using Core.Errors;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly ProductAppService _productSrv;
        private readonly CachedItems _cachedItems;

        public ProductsController(ProductAppService productSrv, CachedItems cachedItems)
        {
            _productSrv = productSrv;
            _cachedItems = cachedItems;
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
        [ProducesResponseType(typeof(ApiErrorResponse<string>), StatusCodes.Status404NotFound)]   //swagger documentation hints
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            return await _productSrv.GetProduct(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productSrv.GetBrands());
        }

        [HttpGet("categories")]
        public ActionResult<IReadOnlyList<ProductBrand>> GetTypes()
        {
            return Ok(_productSrv.GetCategories());
        }

        [HttpGet("cities")]
        public ActionResult<IReadOnlyList<City>> Cities()
        {
            return Ok(_cachedItems.Cities);
        }

        [HttpGet("counties/{id}")]
        public ActionResult<IReadOnlyList<County>> Counties(string id)
        {
            return Ok(_cachedItems.Counties.Where(x => x.City.CityName == id));
        }
    }
}