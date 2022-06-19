using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AccountService _accountSrv;

        public AccountController(AccountService accountSrv)
        {
            _accountSrv = accountSrv;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var response = await _accountSrv.Register(registerDto);
            if (response.StatusCode != 200) return BadRequest(response);
            return response.Data;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var response = await _accountSrv.Login(loginDto);
            if (response.StatusCode != 200) return Unauthorized(response);
            return response.Data;
        }
    }
}