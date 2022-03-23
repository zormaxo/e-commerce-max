using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Repositories;
using Core.Specifications;
using Service.Base;
using Service.Helpers;

namespace Service
{
  public class UserService : BaseService
  {
    private readonly IGenericRepository<AppUser> _appUsersRepo;

    public UserService(IGenericRepository<AppUser> appUsersRepo, IMapper mapper) : base(mapper)
    {
      _appUsersRepo = appUsersRepo;
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