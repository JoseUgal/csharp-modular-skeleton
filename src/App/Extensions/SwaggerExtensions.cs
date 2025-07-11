namespace App.Extensions;

/// <summary>
/// Provides extension methods for Swagger in the application.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swagger services and configuration to the application service collection.
    /// </summary>
    /// <param name="services">The service collection to add Swagger to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddSwaggerWithConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Configures the application to use Swagger middleware and UI.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    /// <returns>The updated application builder.</returns>
    public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}

