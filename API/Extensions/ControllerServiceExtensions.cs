using Core.Interfaces;
using Core.Service.Helpers;
using Service;

namespace API.Extensions
{
  public static class ControllerServiceExtensions
  {
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
      services.AddScoped<ProductService>();
      services.AddScoped<AccountService>();
      services.AddScoped<UserService>();
      services.AddScoped<ITokenService, TokenService>();
      return services;
    }
  }
}
