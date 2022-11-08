using Core.Dtos;
using Core.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Application.ApplicationServices;
using Shop.Application.Extensions;
using System.Net;

namespace Application.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly AccountAppService _accountSrv;
        private IValidator<LoginDto> _validator;

        public AccountController(AccountAppService accountSrv, IValidator<LoginDto> validator)
        {
            _accountSrv = accountSrv;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        { return await _accountSrv.Register(registerDto); }

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
    }
}