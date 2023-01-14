using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Extensions;
using Shop.Application.Interfaces;
using Shop.Core.Entities.Identity;
using Shop.Core.Exceptions;
using Shop.Shared.Dtos;
using System.Net;

namespace Shop.Application.ApplicationServices;

public class AccountAppService : BaseAppService
{
    private readonly ITokenService _tokenService;
    readonly UserManager<AppUser> _userManager;

    public AccountAppService(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) : base(mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<ActionResult<UserDto>> GetCurrentUser(string email)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipalAsync(email);

        return new UserDto { Email = user.Email, Token = await _tokenService.CreateToken(user), FirstName = user.FirstName };
    }

    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        if (await CheckEmailExistsAsync(registerDto.Email))
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Email address is in use");
        }

        var user = _mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Email.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            throw new ApiException(HttpStatusCode.BadRequest, result.Errors);

        var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        if (!roleResult.Succeeded)
            throw new ApiException(HttpStatusCode.BadRequest, result.Errors);

        return new UserDto
        {
            FirstName = user.FirstName,
            Email = user.Email,
            UserId = user.Id,
            Token = await _tokenService.CreateToken(user)
        };
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        var result = await _userManager
            .CheckPasswordAsync(user, loginDto.Password);

        if (!result)
            throw new ApiException(HttpStatusCode.Unauthorized);

        return new UserDto
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            Token = await _tokenService.CreateToken(user),
        };
    }

    private async Task<bool> CheckEmailExistsAsync(string email) { return await _userManager.FindByEmailAsync(email) != null; }
}