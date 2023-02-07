namespace Shop.API.Extensions;

public static class HealthCheckConfigureExtension
{
    public static IApplicationBuilder UseCustomHealthCheck(this IApplicationBuilder app)
    {
        _ = app.UseHealthChecks(
            "/api/health",
            new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, _) => await context.Response.WriteAsync("OK")
            });

        return app;
    }
}
