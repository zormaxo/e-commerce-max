using Service;

namespace API.Extensions
{
  public static class ControllerServiceExtensions
  {
    public static IServiceCollection AddControllerServices(this IServiceCollection services)
    {
      services.AddScoped<ProductService>();
      services.AddScoped<UserService>();
      return services;
    }
  }
}
