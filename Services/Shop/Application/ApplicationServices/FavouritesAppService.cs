using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.Interfaces;

namespace Shop.Application.ApplicationServices;

public class FavouritesAppService : BaseAppService
{
    private readonly IGenericRepository<Favourite> _favRepo;

    public FavouritesAppService(IMapper mapper, IGenericRepository<Favourite> favRepo) : base(mapper) { _favRepo = favRepo; }

    public async Task AddRemoveFavourite(int productId, int userId)
    {
        var fav = await _favRepo.GetAll().FindAsync(productId, userId);

        if (fav == null)
        {
            await _favRepo.AddAsync(new Favourite { LikedProductId = productId, UserId = userId });
        }
        else
        {
            _favRepo.GetAll().Remove(fav);
        }

        await _favRepo.SaveChangesAsync();
    }
}