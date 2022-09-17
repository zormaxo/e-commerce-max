using Application.Entities;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core.Dtos;
using Core.Dtos.Member;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application;

public class UserAppService : BaseAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IPhotoService _photoService;

    public UserAppService(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : base(mapper)
    {
        _userRepository = userRepository;
        _photoService = photoService;
    }

    public async Task<IEnumerable<MemberDto>> GetUsers()
    {
        return await _userRepository.GetMembersAsync();
    }

    public async Task<MemberDto> GetUser(int id)
    {
        return await _userRepository.GetMemberAsync(id);
    }

    public async Task UpdateUser(MemberUpdateDto memberUpdateDto, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        _mapper.Map(memberUpdateDto, user);
        await _userRepository.SaveAllAsync();
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int userId)
    {
        var user = await _userRepository.GetUserByIdIncludePhotoAsync(userId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) throw new ApiException(result.Error.Message);

        var photo = new UserPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllAsync())
        {
            return _mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }
}