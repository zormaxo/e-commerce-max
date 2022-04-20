using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private UserService _userSrv;

        public UsersController(UserService userSrv)
        {
            _userSrv = userSrv;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return Ok(await _userSrv.GetUsers());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            return await _userSrv.GetUser(id);
        }
    }
}