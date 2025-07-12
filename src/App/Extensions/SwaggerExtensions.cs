namespace App.Extensions;

/// <summary>
/// Provides extension methods for Swagger in the application.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Configures the application to use Swagger middleware and UI.
    /// </summary>
    /// <param name="app">The application builder to configure.</param>
    /// <returns>The updated application builder.</returns>
    public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}

