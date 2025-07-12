using Infrastructure.Configuration;
using Quartz;

namespace App.ServiceInstallers.BackgroundJobs;

/// <summary>
/// Represents the background jobs service installer.
/// </summary>
internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<RecurringJobsSetup>();

        services.ConfigureOptions<QuartzHostedServiceOptionsSetup>();

        services.AddQuartz();

        services.AddQuartzHostedService();
    }
}
