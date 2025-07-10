using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace App.Extensions;

/// <summary>
/// Provides extension methods for managing database migrations in the application.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Applies pending database migrations to the application's database.  This ensures that the database schema is up-to-date with the latest migrations.
    /// </summary>
    /// <param name="app">The IApplicationBuilder to configure.</param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
