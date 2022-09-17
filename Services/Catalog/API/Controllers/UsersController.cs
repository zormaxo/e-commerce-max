using Application.Extensions;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            return Ok(await _userSrv.GetUsers());
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(int userId)
        {
            var user = await _userSrv.GetUser(userId);

            return user == null ? BadRequest("User not found") : (ActionResult<MemberDto>)user;
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
            await _userSrv.UpdateUser(memberUpdateDto, User.GetUserName());
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var photoDto = await _userSrv.AddPhoto(file, User.GetUserId());
            return CreatedAtRoute("GetUser", new { userId = User.GetUserId() }, photoDto);
        }

        //[HttpPut("set-main-photo/{photoId}")]
        //public async Task<ActionResult> SetMainPhoto(int photoId)
        //{
        //    var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        //    var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        //    if (photo.IsMain) return BadRequest("This is already your main photo");

        //    var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        //    if (currentMain != null) currentMain.IsMain = false;
        //    photo.IsMain = true;

        //    if (await _userRepository.SaveAllAsync()) return NoContent();

        //    return BadRequest("Failed to set main photo");
        //}

        //[HttpDelete("delete-photo/{photoId}")]
        //public async Task<ActionResult> DeletePhoto(int photoId)
        //{
        //    var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        //    var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        //    if (photo == null) return NotFound();

        //    if (photo.IsMain) return BadRequest("You cannot delete your main photo");

        //    if (photo.PublicId != null)
        //    {
        //        var result = await _photoService.DeletePhotoAsync(photo.PublicId);
        //        if (result.Error != null) return BadRequest(result.Error.Message);
        //    }

        //    user.Photos.Remove(photo);

        //    if (await _userRepository.SaveAllAsync()) return Ok();

        //    return BadRequest("Failed to delete the photo");
        //}
    }
}