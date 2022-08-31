using Application.Entities;

namespace Application.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}