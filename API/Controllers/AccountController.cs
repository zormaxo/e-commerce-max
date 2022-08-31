using Application;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AccountAppService _accountSrv;

        public AccountController(AccountAppService accountSrv)
        {
            _accountSrv = accountSrv;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var response = await _accountSrv.Register(registerDto);
            //if (response.StatusCode != 200) return BadRequest(response);
            return response.Data;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var response = await _accountSrv.Login(loginDto);
            return response.Data;
        }
    }
}