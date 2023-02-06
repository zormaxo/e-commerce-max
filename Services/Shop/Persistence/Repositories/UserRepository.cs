using Microsoft.EntityFrameworkCore;
using Shop.Core.Entities.Identity;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;

namespace Shop.Persistence.Repositories;


public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(StoreContext context) : base(context)
    {
    }

    public Task<AppUser?> GetMemberAsync(int id)
    { return _dbSet.Include(x => x.Photos).Where(x => x.Id == id).SingleOrDefaultAsync(); }

    public Task<PagedList<AppUser>> GetMembersAsync(UserParams userParams)
    {
        var query = _dbSet.Include(x => x.Photos).Where(u => u.UserName != userParams.CurrentUsername);
        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.CreatedDate),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        //var members = query.ProjectTo<AppUser>(_mapper.ConfigurationProvider).AsNoTracking();
        //return await query.AsNoTracking().ToListAsync();
        return PagedList<AppUser>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
    }

    public ValueTask<AppUser> GetUserByIdAsync(int id) { return _context.Users.FindAsync(id); }

    public Task<AppUser> GetUserByIdIncludePhotoAsync(int id)
    { return _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == id); }

    public Task<AppUser> GetUserByUsernameAsync(string username)
    { return _context.Users.Include(p => p.Products).Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username); }

    public Task<List<AppUser>> GetUsersAsync()
    { return _context.Users.Include(p => p.Products).Include(p => p.Photos).ToListAsync(); }

    public async Task<bool> SaveAllAsync() { return await _context.SaveChangesAsync() > 0; }

    public void Update(AppUser user) { _context.Entry(user).State = EntityState.Modified; }
}