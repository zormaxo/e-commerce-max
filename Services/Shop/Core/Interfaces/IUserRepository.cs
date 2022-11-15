using Shop.Core.Entities;
using Shop.Core.HelperTypes;

namespace Shop.Core.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);

    Task<bool> SaveAllAsync();

    Task<List<AppUser>> GetUsersAsync();

    ValueTask<AppUser> GetUserByIdAsync(int id);

    Task<AppUser> GetUserByUsernameAsync(string username);

    Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams);

    Task<AppUser> GetMemberAsync(int id);

    Task<AppUser> GetUserByIdIncludePhotoAsync(int id);
}