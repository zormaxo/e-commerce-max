using Shop.Core.Entities;

namespace Shop.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}