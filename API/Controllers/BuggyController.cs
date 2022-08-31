using API.Errors;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() => "secret text";

        [HttpGet("notfound")]
        public ActionResult<Product> GetNotFoundRequest()
        {
            var thing = _context.Products.Find(-1);

            if (thing == null) return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));

            return Ok(thing);
        }

        [HttpGet("servererror")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Products.Find(-1);

            var thingToReturn = thing.ToString();

            return Ok(thingToReturn);
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "This message is from controller"));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok(id);
        }
    }
}