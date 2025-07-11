using Application.Behaviors;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module application service installer.
/// </summary>
internal sealed class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);

                config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
            }
        );
    }
}
