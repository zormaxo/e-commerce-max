using Application.Extensions;
using Application.Middleware;
using Microsoft.AspNetCore.HttpLogging;
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

            services.AddHttpLogging(
                logging =>
                {
                    logging.LoggingFields = HttpLoggingFields.All;
                    logging.RequestBodyLogLimit = 4096;
                    logging.ResponseBodyLogLimit = 4096;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerDocumentation();
            app.UseSerilogRequestLogging();

            app.UseMiddleware<ResponseWrapperMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseMiddleware<RequestResponseMiddleware>();     //Manuel request response logging

            app.UseStatusCodePagesWithReExecute("/errors/{0}");   //for non-exist endpoints
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpLogging();                                 //.Net core request response logging
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}