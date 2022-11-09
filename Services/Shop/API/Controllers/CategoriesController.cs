using Microsoft.AspNetCore.Mvc;
using Shop.Application.ApplicationServices;
using Shop.Application.Shared.Dtos;

namespace Shop.API.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly CategoriesAppService _productSrv;

    public CategoriesController(CategoriesAppService categoriesSrv) { _productSrv = categoriesSrv; }

    [ResponseCache(Duration = 20)]
    [HttpGet]
    public ActionResult<IReadOnlyList<CategoryDto>> GetCategories() { return Ok(_productSrv.GetCategories()); }
}