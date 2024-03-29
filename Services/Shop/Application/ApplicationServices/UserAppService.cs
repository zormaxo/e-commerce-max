using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Common.Interfaces.Photo;
using Shop.Core.Entities;
using Shop.Core.Entities.Identity;
using Shop.Core.Exceptions;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos;
using Shop.Shared.Dtos.Member;
using System.Net;

namespace Shop.Application.ApplicationServices;

public class UserAppService : BaseAppService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IPhotoService _photoService;

    public UserAppService(
        IUserRepository userRepository,
        IPhotoService photoService,
        IServiceProvider serviceProvider,
        UserManager<AppUser> userManager) : base(serviceProvider)
    {
        _userRepository = userRepository;
        _photoService = photoService;
        _userManager = userManager;
    }

    public async Task<PagedList<MemberDto>> GetUsers(UserParams userParams, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        userParams.CurrentUsername = user.UserName!;

        PagedList<AppUser> appUsers = await _userRepository.GetMembersAsync(userParams);

        return Mapper.Map<PagedList<MemberDto>>(appUsers);
    }

    public async Task<MemberDto> GetUser(int id)
    {
        var user = await _userRepository.GetMemberAsync(id);
        return Mapper.Map<MemberDto>(user);
    }

    public async Task UpdateUser(MemberUpdateDto memberUpdateDto, string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
            throw new ApiException("User not found");

        Mapper.Map(memberUpdateDto, user);
        user.UserName = user.Email;

        await _userManager.UpdateAsync(user);
    }

    public async Task<PhotoDto> AddPhoto(IFormFile file, int userId)
    {
        var user = await _userRepository.GetUserByIdIncludePhotoAsync(userId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            throw new ApiException(result.Error.Message);

        var photo = new UserPhoto { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllAsync())
        {
            return Mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }

    public async Task SetMainPhoto(int photoId, int userId)
    {
        var user = await _userRepository.GetUserByIdIncludePhotoAsync(userId);

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo is null || photo.IsMain)
            throw new ApiException("This is already your main photo");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null)
            currentMain.IsMain = false;
        photo.IsMain = true;

        if (await _userRepository.SaveAllAsync())
            return;

        throw new ApiException("Failed to set main photo");
    }

    public async Task<PhotoDto> AddPhotoAndSetMain(IFormFile file, int userId)
    {
        var user = await _userRepository.GetUserByIdIncludePhotoAsync(userId);

        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null)
            throw new ApiException(result.Error.Message);

        var photos = user.Photos.ToList();
        photos.ForEach(x => x.IsMain = false);

        var photo = new UserPhoto { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId, IsMain = true };

        user.Photos.Add(photo);

        if (await _userRepository.SaveAllAsync())
        {
            return Mapper.Map<PhotoDto>(photo);
        }

        throw new ApiException("Problem addding photo");
    }

    public async Task DeletePhoto(int photoId, int userId)
    {
        var user = await _userRepository.GetUserByIdIncludePhotoAsync(userId);

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null)
            throw new ApiException(HttpStatusCode.NotFound);

        if (photo.IsMain)
            throw new ApiException("You cannot delete your main photo");

        if (photo.PublicId != null)
        {
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null)
                throw new ApiException(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await _userRepository.SaveAllAsync())
            return;

        throw new ApiException("Failed to delete the photo");
    }
}