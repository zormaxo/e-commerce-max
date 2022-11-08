using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Core.Dtos;

namespace Application.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly CategoriesAppService _productSrv;

        public CategoriesController(CategoriesAppService categoriesSrv) { _productSrv = categoriesSrv; }


        [HttpGet]
        public ActionResult<IReadOnlyList<CategoryDto>> GetCategories() { return Ok(_productSrv.GetCategories()); }
    }
}