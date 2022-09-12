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
            return await _accountSrv.Register(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return await _accountSrv.Login(loginDto);
        }
    }
}