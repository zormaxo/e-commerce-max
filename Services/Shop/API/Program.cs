using AutoMapper;
using Microsoft.Extensions.FileProviders;
using RestSharp;
using Serilog;
using Shop.API.Extensions;
using Shop.API.Middleware;
using Shop.Application;
using Shop.Application.Common.Interfaces.Repository;
using Shop.Application.Extensions;
using Shop.Application.SignalR;
using Shop.Core.HelperTypes;
using Shop.Persistence;
using System.Text.Json.Serialization;

Log.Information("Application Starting Up");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllerServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseCors("CorsPolicy");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();
var context = services.GetRequiredService<IStoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
var restClient = services.GetRequiredService<RestClient>();
var mapper = services.GetRequiredService<IMapper>();
var cahcedItems = services.GetRequiredService<CachedItems>();
try
{
    var initialiser = scope.ServiceProvider.GetRequiredService<StoreContextSeed>();
    await initialiser.SeedAsync();
    await CacheService.FillCacheItemsAsync(context, loggerFactory, mapper, cahcedItems, restClient);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

Log.Information("Application Started Up");
app.Run();