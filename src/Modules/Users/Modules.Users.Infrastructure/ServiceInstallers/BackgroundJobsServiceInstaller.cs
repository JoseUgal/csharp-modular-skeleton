using Infrastructure.BackgroundJobs;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

namespace Modules.Users.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the users module background jobs service installer.
/// </summary>
internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<ProcessOutboxMessagesOptionsSetup>();

        services.AddRecurringJobConfigurations(AssemblyReference.Assembly);
    }
}
