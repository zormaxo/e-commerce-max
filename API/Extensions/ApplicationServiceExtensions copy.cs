using API.Errors;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Helpers;

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
