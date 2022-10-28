using AutoMapper;
using Core.Dtos;
using Core.Dtos.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Extensions;
using Shop.Application.Extensions;
using Shop.Core.HelperTypes;

namespace Application.Controllers
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
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await _userSrv.GetUsers(userParams, User.GetUserName());
            Response.AddPaginationHeader(users.PageIndex, users.PageSize, users.TotalCount, users.TotalPages);
            return users;
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(int userId)
        {
            var user = await _userSrv.GetUser(userId);

            return user == null ? BadRequest("User not found") : (ActionResult<MemberDto>)user;
        }

        [HttpPut("update-member")]
        public async Task UpdateMember(MemberUpdateDto memberUpdateDto) { await UpdateUser(memberUpdateDto); }

        [HttpPut("update-user-first-last-name")]
        public async Task UpdateUserFirstLastName(MemberNameUpdateDto memberNameUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberNameUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        [HttpPut("update-username")]
        public async Task UpdateUsername(MemberUsernameUpdateDto memberUsernameUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberUsernameUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        [HttpPut("update-user-phone")]
        public async Task UpdateUserPhone(MemberPhoneUpdateDto memberPhoneUpdateDto)
        {
            var memberUpdateDto = new MemberUpdateDto();
            _mapper.Map(memberPhoneUpdateDto, memberUpdateDto);

            await UpdateUser(memberUpdateDto);
        }

        private async Task UpdateUser(MemberUpdateDto memberUpdateDto)
        { await _userSrv.UpdateUser(memberUpdateDto, User.GetUserName()); }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var photoDto = await _userSrv.AddPhoto(file, User.GetUserId());
            return CreatedAtRoute("GetUser", new { userId = User.GetUserId() }, photoDto);
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            await _userSrv.SetMainPhoto(photoId, User.GetUserId());
            return NoContent();
        }

        [HttpPost("add-photo-and-set-main")]
        public async Task<ActionResult> AddPhotoAndSetMain(IFormFile file)
        {
            var photoDto = await _userSrv.AddPhotoAndSetMain(file, User.GetUserId());
            return CreatedAtRoute("GetUser", new { userId = User.GetUserId() }, photoDto);
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            await _userSrv.DeletePhoto(photoId, User.GetUserId());
            return Ok();
        }
    }
}