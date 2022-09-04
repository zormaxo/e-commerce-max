using Application;
using Application.Entities;
using Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers
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

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() => "secret text";

        [HttpGet("notfound")]
        public ActionResult<Product> GetNotFoundRequest()
        {
            var thing = _context.Products.Find(-1);

            if (thing == null) return NotFound(new ApiErrorResponse("Not Found Kuyumdan"));

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
            return BadRequest(new ApiErrorResponse("This message is from controller"));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok(id);
        }

        [HttpGet("currency")]
        public async Task<ActionResult<string>> GetCurrency()
        {
            var httpResponseMessage = await _client.GetAsync(
                $"https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");
            var response = await httpResponseMessage.Content.ReadAsStringAsync();

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(response);
            return JsonSerializer.Serialize(jsonElement, new JsonSerializerOptions() { WriteIndented = true });
        }
    }
}