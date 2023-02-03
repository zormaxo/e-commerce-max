using Microsoft.AspNetCore.Mvc;
using Shop.API.Filters;

namespace Shop.API.Controllers;

[ServiceFilter(typeof(LogUserActivityAttribute))]
[ServiceFilter(typeof(ResponseFilterAttribute))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}