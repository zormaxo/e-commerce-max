using Microsoft.AspNetCore.Mvc;
using Shop.API.Response;
using Shop.Application.ActionFilters;

namespace Shop.API.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ServiceFilter(typeof(SampleActionFilter))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}