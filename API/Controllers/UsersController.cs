using Application;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserAppService _userSrv;

        public UsersController(UserAppService userSrv)
        {
            _userSrv = userSrv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _userSrv.GetUsers());
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<MemberDto>> GetUser(int userId)
        {
            return await _userSrv.GetUser(userId);
        }

        [HttpPut]
        public async Task UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _userSrv.UpdateUser(memberUpdateDto, username);
        }
    }
}