using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Core.Entities;
using Shop.Shared.Dtos;
using System.Text.Json;

namespace Shop.API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly IStoreContext _context;
    private readonly HttpClient _client;
    private readonly IHttpClientFactory _httpClientFactory;

    public BuggyController(IStoreContext context, HttpClient client, IHttpClientFactory HttpClientFactory)
    {
        _context = context;
        _client = client;
        _httpClientFactory = HttpClientFactory;
    }


    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret() => "secret text";

    [HttpGet("notfound")]
    public ActionResult<Product> GetNotFoundRequest()
    {
        var thing = _context.Products.Find(-1);

        if (thing == null)
            return NotFound("Not Found Kuyumdan");

        return Ok(thing);
    }

    [HttpGet("servererror")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Products.Find(-1);

        var thingToReturn = thing!.ToString();

        return Ok(thingToReturn);
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest() { return BadRequest("This message is from controller"); }

    [HttpGet("currency")]
    public async Task<ActionResult<JsonElement>> GetCurrency()
    {
        var uri = $"https://api.currencyfreaks.com/latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD";

        var httpResponseMessage = await _client.GetAsync(uri);
        var response = await httpResponseMessage.Content.ReadAsStringAsync();
        var reponse2 = await _client.GetStringAsync(uri);

        var client = _httpClientFactory.CreateClient("currencyfreak");
        var reponse3 = await client.GetStringAsync($"latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");

        var reponse4 = await client.GetFromJsonAsync<CurrencyFreakDto>(
            $"latest?apikey=931ffa032f6b426fade0f8ffd6b74396&symbols=TRY,GBP,EUR,USD");

        return JsonSerializer.Deserialize<JsonElement>(response);
    }
}