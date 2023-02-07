using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using RestSharp;
using Serilog;
using Shop.API.Extensions;
using Shop.API.Middleware;
using Shop.Application;
using Shop.Application.Extensions;
using Shop.Application.SignalR;
using Shop.Core.Entities.Identity;
using Shop.Core.HelperTypes;
using Shop.Persistence;
using System.Text.Json.Serialization;

Log.Information("Application Starting Up");
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddControllerServices(builder.Configuration, builder.Environment);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseCustomHealthCheck();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
        RequestPath = "/Content"
    });

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var context = services.GetRequiredService<StoreContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
var logger = services.GetRequiredService<ILogger<Program>>();
var restClient = services.GetRequiredService<RestClient>();
var mapper = services.GetRequiredService<IMapper>();
var cahcedItems = services.GetRequiredService<CachedItems>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, userManager, roleManager, loggerFactory);
    await CacheService.FillCacheItemsAsync(context, loggerFactory, mapper, cahcedItems, restClient);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

Log.Information("Application Started Up");
app.Run();