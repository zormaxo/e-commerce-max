using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.API.Validation;
using Shop.Application.ApplicationServices;
using Shop.Core.Exceptions;
using Shop.Shared.Dtos.Account;
using System.Net;
using System.Security.Claims;

namespace Shop.API.Controllers;

public class AccountController : BaseApiController
{
    private readonly AccountAppService _accountSrv;
    private readonly IValidator<LoginDto> _validator;

    public AccountController(AccountAppService accountSrv, IValidator<LoginDto> validator)
    {
        _accountSrv = accountSrv;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) { return await _accountSrv.Register(registerDto); }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        ValidationResult result = await _validator.ValidateAsync(loginDto);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            throw new ApiException(HttpStatusCode.Unauthorized, JsonConvert.SerializeObject(ModelState));
        }
        return await _accountSrv.Login(loginDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return await _accountSrv.GetCurrentUser(email);
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    { return await _accountSrv.CheckEmailExistsAsync(email); }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return await _accountSrv.GetUserAddress(email);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return await _accountSrv.UpdateUserAddress(address, email);
    }
}