using Microsoft.EntityFrameworkCore;
using Modules.Users.Infrastructure.Database;

namespace App.StartupTasks;

/// <summary>
/// Represents the startup task for migrating the database in the development environment only.
/// </summary>
internal sealed class MigrateDatabaseStartupTask(
    IHostEnvironment hostEnvironment,
    IServiceProvider serviceProvider
) : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return;
        }

        using var scope = serviceProvider.CreateScope();

        await MigrateDatabaseAsync<UsersDbContext>(scope, stoppingToken);
    }

    private static async Task MigrateDatabaseAsync<TDbContext>(IServiceScope scope, CancellationToken cancellationToken) where TDbContext : DbContext
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
