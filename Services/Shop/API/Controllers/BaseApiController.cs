using Microsoft.AspNetCore.Mvc;
using Shop.Application.ActionFilters;

namespace Shop.API.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}