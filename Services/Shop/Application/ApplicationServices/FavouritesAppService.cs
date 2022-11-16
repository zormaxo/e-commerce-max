using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.Interfaces;

namespace Shop.Application.ApplicationServices;

public class FavouritesAppService : BaseAppService
{
    private readonly IGenericRepository<Favourite> _favRepo;

    public FavouritesAppService(IMapper mapper, IGenericRepository<Favourite> favRepo) : base(mapper) { _favRepo = favRepo; }

    public async Task AddFavourite(int productId, int userId)
    {
        await _favRepo.AddAsync(new Favourite { LikedProductId = productId, UserId = userId });
        await _favRepo.SaveChangesAsync();
    }
}