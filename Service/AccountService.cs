using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Repositories;

namespace Service
{
  public class AccountService : BaseService
  {
    private readonly IGenericRepository<AppUser> _appUsersRepo;

    public AccountService(IGenericRepository<AppUser> appUsersRepo, IMapper mapper) : base(mapper)
    {
      _appUsersRepo = appUsersRepo;
    }

    // public async Task<UserDto> Register(RegisterDto registerDto)
    // {
    //   if (await UserExists(registerDto.Username)) return ObjectResult("Username is taken");

    //   using var hmac = new HMACSHA512();

    //   var user = new AppUser
    //   {
    //     UserName = registerDto.Username.ToLower(),
    //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
    //     PasswordSalt = hmac.Key
    //   };

    //   _context.Users.Add(user);
    //   await _context.SaveChangesAsync();

    //   return new UserDto
    //   {
    //     Username = user.UserName,
    //     Token = _tokenService.CreateToken(user)
    //   };
    // }

    private async Task<bool> UserExists(string username)
    {
      return await _appUsersRepo.AnyAsync(x => x.UserName == username.ToLower());
    }

    public async Task<IEnumerable<AppUser>> GetUsers()
    {
      return await _appUsersRepo.ListAllAsync();
    }

    public async Task<AppUser> GetUser(int id)
    {
      return await _appUsersRepo.GetByIdAsync(id);
    }
  }
}