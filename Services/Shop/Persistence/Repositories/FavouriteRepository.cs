using Shop.Application.Shared.Dtos;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Persistence;
using Shop.Persistence.Repositories;
using Shop.Persistence.Services;

namespace API.Data;

public class FavouriteRepository : GenericRepository<Favourite>, IFavouriteRepository
{
    readonly UserResolverService _userService;
    public FavouriteRepository(StoreContext context, UserResolverService userService) : base(context)
    { _userService = userService; }

    public async Task<Favourite> GetUserLike(int sourceUserId, int likedUserId)
    { return await _context.Favourites.FindAsync(sourceUserId, likedUserId); }

    //public async Task<PagedList<FavouriteDto>> GetFavourites()
    //{
    //    var userId = _userService.GetUserId();


    //}
}