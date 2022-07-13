using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserService _userSrv;

        public UsersController(UserService userSrv)
        {
            _userSrv = userSrv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var response = await _userSrv.GetUsers();
            if (response.StatusCode != 200) return BadRequest(response);
            return Ok(response.Data);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<MemberDto>> GetUser(int userId)
        {
            var response = await _userSrv.GetUser(userId);
            if (response.StatusCode != 200) return BadRequest(response);
            return Ok(response.Data);
        }
    }
}