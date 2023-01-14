using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using RestSharp;
using Shop.API.Extensions;
using Shop.API.Middleware;
using Shop.API.SignalR;
using Shop.Application.Extensions;
using Shop.Application.SignalR;
using Shop.Infrastructure.Extensions;
using Shop.Persistence.Extensions;

namespace Shop.API;

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
        services.AddHttpClient(
            "currencyfreak",
            config =>
            {
                config.BaseAddress = new Uri("https://api.currencyfreaks.com/");
            });

        services.AddSingleton(new RestClient(new HttpClient()));

        services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddOtherServices();
        services.AddApplicationServices(_config);
        services.AddPersistenceServices(_config, _env.IsProduction());
        services.AddInfrastructureServices(_config);
        services.AddControllerServices();
        services.AddSwaggerDocumentation();
        services.AddCors(
            opt => opt.AddPolicy(
                "CorsPolicy",
                policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200")));
        services.AddIdentityServices(_config);
        services.AddSignalR();
        services.AddHttpContextAccessor();

        services.AddHttpLogging(
            logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

        services.AddHealthChecks();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        app.UseCustomHealthCheck();
        app.UseSwaggerDocumentation();
        //app.UseSerilogRequestLogging();

        app.UseMiddleware<ResponseWrapperMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        //app.UseMiddleware<RequestResponseMiddleware>();     //Manuel request response logging

        app.UseStatusCodePagesWithReExecute("/errors/{0}");   //for non-exist endpoints
        app.UseResponseCaching();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseHttpLogging();                                 //.Net core request response logging
        app.UseRouting();
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200"));
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PresenceHub>("hubs/presence");
                endpoints.MapHub<MessageHub>("hubs/message");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
    }
}