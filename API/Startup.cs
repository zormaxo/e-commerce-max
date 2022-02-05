using API.Extensions;
using API.Middleware;
using Core.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.Helpers;

namespace Service.BaseService
{
  public class Startup
  {
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAutoMapper(typeof(MappingProfiles));
      services.AddControllers();
      services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
      services.AddApplicationServices();
      services.AddSwaggerDocumentation();
      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy =>
            {
              policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ExceptionMiddleware>();
      app.UseSwaggerDocumentation();
      app.UseStatusCodePagesWithReExecute("/errors/{0}");  //for non-exist endpoints
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseCors("CorsPolicy");
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
