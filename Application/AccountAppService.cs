using Application.Interfaces;
using Application.Services;
using Application.Specifications;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Application;

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
        if (await UserExists(registerDto.Username))
            throw new ApiException(HttpStatusCode.BadRequest, "Username is taken");

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            FirstName = registerDto.FirstName,
            Surname = registerDto.Surname,
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        await _appUsersRepo.AddAsync(user);
        await _appUsersRepo.SaveChangesAsync();

        var userDto = new UserDto
        {
            FirstName = user.FirstName,
            UserId = user.Id,
            Token = _tokenService.CreateToken(user)
        };

        return userDto;
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var spec = new UsersSpecification(loginDto.Username);
        var user = await _appUsersRepo.GetEntityWithSpec(spec);

        if (user == null)
            throw new ApiException(HttpStatusCode.Unauthorized, "Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                throw new ApiException(HttpStatusCode.Unauthorized, "Invalid password");
        }

        var userDto = new UserDto
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            Token = _tokenService.CreateToken(user)
        };

        return userDto;
    }

    private async Task<bool> UserExists(string username)
    {
        return await _appUsersRepo.AnyAsync(x => x.Username == username.ToLower());
    }
}