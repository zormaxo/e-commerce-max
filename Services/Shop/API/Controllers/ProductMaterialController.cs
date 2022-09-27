using Application.Entities;
using Application.Extensions;
using Application.Specifications;
using Core.Dtos;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers;

namespace Application.Controllers
{
    public class ProductsMaterialController : BaseApiController
    {
        private readonly ProductMaterialAppService _productMachineSrv;
        private readonly CachedItems _cachedItems;

        public ProductsMaterialController(ProductMaterialAppService productMaterialSrv, CachedItems cachedItems)
        {
            _productMachineSrv = productMaterialSrv;
            _cachedItems = cachedItems;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productMachineSrv.GetProducts(productParams));
        }

        [HttpGet("product-counts")]
        public async Task<ActionResult<object>> GetProductCounts([FromQuery] ProductSpecParams productParams)
        {
            return Ok(await _productMachineSrv.GetProductsCounts(productParams));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiErrorResponse<string>), StatusCodes.Status404NotFound)]   //swagger documentation hints
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            return await _productMachineSrv.GetProduct(id);
        }

        [HttpPost("update-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productMachineSrv.UpdateProduct(product);
        }

        [HttpGet("categories")]
        public ActionResult<IReadOnlyList<ProductBrand>> GetTypes()
        {
            return Ok(_productMachineSrv.GetCategories());
        }

        [HttpGet("cities")]
        public ActionResult<IReadOnlyList<City>> Cities()
        {
            return Ok(_cachedItems.Cities);
        }

        [HttpGet("counties/{id}")]
        public ActionResult<IReadOnlyList<County>> Counties(string id)
        {
            return Ok(_cachedItems.Counties.Where(x => x.City.Name == id));
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            return await _productMachineSrv.AddPhoto(file, User.GetUserId());
        }
    }
}