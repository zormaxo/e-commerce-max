using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly CategoriesAppService _productSrv;

        public CategoriesController(CategoriesAppService categoriesSrv)
        {
            _productSrv = categoriesSrv;
        }


        [HttpGet()]
        public ActionResult<IReadOnlyList<ProductBrand>> GetTypes()
        {
            return Ok(_productSrv.GetCategories());
        }
    }
}