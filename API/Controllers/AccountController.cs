using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private AccountService _accountSrv;
    public AccountController(AccountService accountSrv)
    {
      _accountSrv = accountSrv;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      UserDto user = await _accountSrv.Register(registerDto);

      return user ?? new ActionResult<UserDto>(BadRequest("Username is taken"));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      UserDto user = await _accountSrv.Login(loginDto);
      return user ?? new ActionResult<UserDto>(BadRequest("Invalid"));
    }
  }
}