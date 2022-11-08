using Core.Entities;
using Shop.Core.HelperTypes;

namespace Shop.Infrastructure.Repositories;

public interface IUserRepository
{
    void Update(AppUser user);

    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<AppUser> GetUserByIdAsync(int id);

    Task<AppUser> GetUserByUsernameAsync(string username);

    Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams);

    Task<AppUser> GetMemberAsync(int id);

    Task<AppUser> GetUserByIdIncludePhotoAsync(int id);
}