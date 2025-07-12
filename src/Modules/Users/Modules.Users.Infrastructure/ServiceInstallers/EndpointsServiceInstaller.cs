using Application.Behaviors;
using Endpoints.Extensions;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module endpoints service installer.
/// </summary>
internal sealed class EndpointsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Endpoints.AssemblyReference.Assembly);
    }
}
