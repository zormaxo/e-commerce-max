using Microsoft.OpenApi.Models;

namespace Shop.API.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }));

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        //// app.UseDeveloperExceptionPage(); //we don't use anymore
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        return app;
    }
}