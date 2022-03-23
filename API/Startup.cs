using API.Extensions;
using API.Middleware;

namespace Service.BaseService
{
  public class Startup
  {
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
      _config = config;
      _env = env;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApplicationServices(_env, _config);
      services.AddControllers();
      services.AddSwaggerDocumentation();
      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); });
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
