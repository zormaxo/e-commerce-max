using Shop.Core.Entities.Identity;

namespace Shop.Application.Common.Interfaces.Authentication;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}