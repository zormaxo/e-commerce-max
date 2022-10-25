using Core.Dtos.Member;
using Core.Entities;
using Shop.Core.HelperTypes;

namespace Shop.Core.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);

    Task<bool> SaveAllAsync();

    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<AppUser> GetUserByIdAsync(int id);

    Task<AppUser> GetUserByUsernameAsync(string username);

    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);

    Task<MemberDto> GetMemberAsync(int id);

    Task<AppUser> GetUserByIdIncludePhotoAsync(int id);
}