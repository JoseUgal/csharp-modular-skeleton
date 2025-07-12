using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Modules.Users.Persistence;
using Modules.Users.Persistence.Constants;
using Persistence.Extensions;
using Persistence.Interceptors;
using Persistence.Options;

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

        //services.AddTransientAsMatchingInterfaces(Persistence.AssemblyReference.Assembly);

        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.TryAddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
            {
                var connectionString = sp.GetService<IOptions<ConnectionStringOptions>>()!.Value;

                options.UseNpgsql(connectionString, config =>
                    config.WithMigrationHistoryTableInSchema(Schemas.Users)
                ).UseUpperSnakeCaseNamingConvention();

                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                    sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>()
                );
            }
        );
    }
}
