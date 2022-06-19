using Core.DTOs;
using Core.Entities;
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


        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var response = await _userSrv.GetUser(username);
            if (response.StatusCode != 200) return BadRequest(response);
            return Ok(response.Data);
        }
    }
}