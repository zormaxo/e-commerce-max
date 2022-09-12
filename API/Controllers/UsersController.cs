using Application;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}