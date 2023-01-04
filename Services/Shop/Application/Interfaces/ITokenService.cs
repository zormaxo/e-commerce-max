using Shop.Core.Entities.Identity;

namespace Shop.Application.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}