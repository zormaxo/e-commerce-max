using Application;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Application.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly HttpClient _client;

        public BuggyController(StoreContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This message is from controller");
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() => "secret text";

        [HttpGet("notfound")]
        public ActionResult<Product> GetNotFoundRequest()
        {
            var thing = _context.Products.Find(-1);

            if (thing == null) return NotFound("Not Found Kuyumdan");

            return Ok(thing);
        }

        [HttpGet("servererror")]
        public ActionResult<string> GetServerError()
        {
            var thing = _context.Products.Find(-1);

            var thingToReturn = thing.ToString();

            return Ok(thingToReturn);
        }

        [HttpGet("currency")]
        public async Task<ActionResult<JsonElement>> GetCurrency()
        {
            var httpResponseMessage = await _client.GetAsync(
                "https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<JsonElement>(response);
        }
    }
}