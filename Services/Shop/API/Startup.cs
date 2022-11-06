using Application.Extensions;
using Application.Middleware;
using Serilog;
using System.Text.Json.Serialization;

namespace Application
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
            services.AddHttpClient();
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddApplicationServices(_env, _config);
            services.AddControllerServices();
            services.AddSwaggerDocumentation();
            services.AddCors(
                opt => opt.AddPolicy(
                    "CorsPolicy",
                    policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200")));
            services.AddIdentityServices(_config);
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerDocumentation();

            app.UseMiddleware<ResponseWrapperMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");  //for non-exist endpoints
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}