using Application;
using Application.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Shop.Core.HelperTypes;

namespace Shop.Infrastructure.Repositories;


public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(StoreContext context) : base(context)
    {
    }

    public async Task<AppUser> GetMemberAsync(int id)
    {
        return await _context.Users.Where(x => x.Id == id).SingleOrDefaultAsync();

        //return await _context.Users
        //    .Where(x => x.Id == id)
        //    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        //    .SingleOrDefaultAsync();
    }

    //public async Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams)
    //{
    //    var query = _context.Users.Where(u => u.UserName != userParams.CurrentUsername);
    //    query = userParams.OrderBy switch
    //    {
    //        "created" => query.OrderByDescending(u => u.CreatedDate),
    //        _ => query.OrderByDescending(u => u.LastActive)
    //    };

    //    //var members = query.ProjectTo<AppUser>(_mapper.ConfigurationProvider).AsNoTracking();

    //    return await PagedList<AppUser>.CreateAsync(query, userParams.PageIndex, userParams.PageSize);
    //}

    public async Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.Where(u => u.UserName != userParams.CurrentUsername);
        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.CreatedDate),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        //var members = query.ProjectTo<AppUser>(_mapper.ConfigurationProvider).AsNoTracking();
        //return await query.AsNoTracking().ToListAsync();
        return await PagedList<AppUser>.CreateAsync(query, userParams.PageIndex, userParams.PageSize);
    }

    public async Task<AppUser> GetUserByIdAsync(int id) { return await _context.Users.FindAsync(id); }

    public async Task<AppUser> GetUserByIdIncludePhotoAsync(int id)
    { return await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == id); }

    public async Task<AppUser> GetUserByUsernameAsync(string username)
    { return await _context.Users.Include(p => p.Products).SingleOrDefaultAsync(x => x.UserName == username); }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    { return await _context.Users.Include(p => p.Products).ToListAsync(); }

    public async Task<bool> SaveAllAsync() { return await _context.SaveChangesAsync() > 0; }

    public void Update(AppUser user) { _context.Entry(user).State = EntityState.Modified; }
}