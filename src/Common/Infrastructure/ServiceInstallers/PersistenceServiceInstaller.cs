using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Options;

namespace Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the persistence service installer.
/// </summary>
internal sealed class PersistenceServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<ConnectionStringSetup>();

        services.AddTransientAsMatchingInterfaces(Persistence.AssemblyReference.Assembly);

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}

