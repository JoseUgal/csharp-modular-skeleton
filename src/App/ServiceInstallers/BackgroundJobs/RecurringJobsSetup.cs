using Domain.Extensions;
using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace App.ServiceInstallers.BackgroundJobs;

/// <summary>
/// Represents the <see cref="QuartzOptions"/> setup.
/// </summary>
internal sealed class RecurringJobsSetup(
    IEnumerable<IRecurringJobConfiguration> recurringJobConfigurations
) : IConfigureOptions<QuartzOptions>
{
    /// <inheritdoc />
    public void Configure(QuartzOptions options) =>
        recurringJobConfigurations.ForEach(configuration =>
            options
                .AddJob(
                    configuration.Type,
                    jobBuilder => jobBuilder.WithIdentity(configuration.Name)
                )
                .AddTrigger(triggerBuilder =>
                    triggerBuilder
                        .ForJob(configuration.Name)
                        .WithSimpleSchedule(scheduleBuilder =>
                            scheduleBuilder
                                .WithIntervalInSeconds(configuration.IntervalInSeconds)
                                .RepeatForever()
                        )
                )
        );
}
