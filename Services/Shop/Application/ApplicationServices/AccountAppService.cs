using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Interfaces;
using Shop.Application.Shared.Dtos;
using Shop.Core.Entities;
using Shop.Core.Exceptions;
using Shop.Core.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Application.ApplicationServices;

public class AccountAppService : BaseAppService
{
    private readonly IGenericRepository<AppUser> _appUsersRepo;
    private readonly ITokenService _tokenService;

    public AccountAppService(IGenericRepository<AppUser> appUsersRepo, IMapper mapper, ITokenService tokenService) : base(mapper)
    {
        _appUsersRepo = appUsersRepo;
        _tokenService = tokenService;
    }

    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.UserName))
            throw new ApiException(HttpStatusCode.BadRequest, "Username is taken");

        var user = _mapper.Map<AppUser>(registerDto);

        using var hmac = new HMACSHA512();

        user.UserName = registerDto.UserName.ToLower();
        user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        user.PasswordSalt = hmac.Key;
        await _appUsersRepo.AddAsync(user);

        await _appUsersRepo.SaveChangesAsync();

        var userDto = new UserDto { FirstName = user.FirstName, UserId = user.Id, Token = _tokenService.CreateToken(user) };

        return userDto;
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var user = await _appUsersRepo.GetAll().Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null)
            throw new ApiException(HttpStatusCode.Unauthorized, "There is no such a username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                throw new ApiException(HttpStatusCode.Unauthorized, "Invalid password");
        }

        var userDto = new UserDto { UserId = user.Id, FirstName = user.FirstName, Token = _tokenService.CreateToken(user), };

        return userDto;
    }

    private async Task<bool> UserExists(string userName)
    { return await _appUsersRepo.AnyAsync(x => x.UserName == userName.ToLower()); }
}