using Application;
using Application.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Member;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;

namespace Shop.Infrastructure.Repositories;


public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    private readonly IMapper _mapper;

    public UserRepository(StoreContext context, IMapper mapper) : base(context) { _mapper = mapper; }

    public async Task<MemberDto> GetMemberAsync(int id)
    {
        return await _context.Users
            .Where(x => x.Id == id)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
    {
        var query = _context.Users.Where(u => u.UserName != userParams.CurrentUsername);
        query = userParams.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.CreatedDate),
            _ => query.OrderByDescending(u => u.LastActive)
        };

        var members = query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking();

        return await PagedList<MemberDto>.CreateAsync(members, userParams.PageIndex, userParams.PageSize);
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