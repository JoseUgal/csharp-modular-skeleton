using Application.Data;
using Infrastructure.Configuration;
using Infrastructure.Database.Extensions;
using Infrastructure.Database.Interceptors;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Infrastructure.Database;
using Modules.Users.Infrastructure.Database.Constants;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module database service installer.
/// </summary>
internal sealed class DatabaseServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(
                connectionString,
                "The database connection string is required."
            );
        }

        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString, config =>
                    config.WithMigrationHistoryTableInSchema(Schemas.Users)
                ).UseUpperSnakeCaseNamingConvention();

                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()
                );
            }
        );
    }
}
