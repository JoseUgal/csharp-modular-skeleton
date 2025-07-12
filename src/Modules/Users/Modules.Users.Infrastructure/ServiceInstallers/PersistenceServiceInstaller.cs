using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Persistence;
using Modules.Users.Persistence.Constants;
using Persistence.Extensions;
using Persistence.Interceptors;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module database service installer.
/// </summary>
internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScopedAsMatchingInterfaces(Persistence.AssemblyReference.Assembly);

        services.AddTransientAsMatchingInterfaces(Persistence.AssemblyReference.Assembly);

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
