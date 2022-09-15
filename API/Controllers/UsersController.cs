using Application;
using AutoMapper;
using Core.Dtos.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserAppService _userSrv;
        private readonly IMapper _mapper;

        public UsersController(UserAppService userSrv, IMapper mapper)
        {
            _userSrv = userSrv;
            _mapper = mapper;
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

        [HttpPut("updateUserFirstLastName")]
        public async Task UpdateUserFirstLastName(MemberNameUpdateDto memberNameUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberNameUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        [HttpPut("updateUsername")]
        public async Task UpdateUsername(MemberUsernameUpdateDto memberUsernameUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberUsernameUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        [HttpPut("updateUserPhone")]
        public async Task UpdateUserPhone(MemberPhoneUpdateDto memberPhoneUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberPhoneUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        private async Task UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            await _userSrv.UpdateUser(memberUpdateDto, username);
        }
    }
}