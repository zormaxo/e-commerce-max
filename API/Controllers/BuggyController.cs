using System.Net;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class BuggyController : BaseApiController
  {
    private readonly StoreContext _context;
    public BuggyController(StoreContext context)
    {
      _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
      var thing = _context.Products.Find(42);

      if (thing == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

      return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
      var thing = _context.Products.Find(42);

      var thingToReturn = thing.ToString();

      return Ok(thingToReturn);
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
      return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
      return Ok(id);
    }
  }
}