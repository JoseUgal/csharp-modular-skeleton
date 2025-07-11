using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Infrastructure;

/// <summary>
/// Represents the users module installer.
/// </summary>
public sealed class UsersModuleInstaller : IModuleInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.InstallServicesFromAssemblies(configuration, AssemblyReference.Assembly);

        services.AddTransientAsMatchingInterfaces(AssemblyReference.Assembly);

        services.AddScopedAsMatchingInterfaces(AssemblyReference.Assembly);
    }
}
