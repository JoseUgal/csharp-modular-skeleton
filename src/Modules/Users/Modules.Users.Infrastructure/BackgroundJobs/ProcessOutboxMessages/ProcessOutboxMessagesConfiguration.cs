using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesJob"/> configuration.
/// </summary>
internal sealed class ProcessOutboxMessagesConfiguration(IOptions<ProcessOutboxMessagesOptions> options) : IRecurringJobConfiguration
{
    /// <inheritdoc />
    public string Name => typeof(ProcessOutboxMessagesJob).FullName!;

    /// <inheritdoc />
    public Type Type => typeof(ProcessOutboxMessagesJob);

    /// <inheritdoc />
    public int IntervalInSeconds => options.Value.IntervalInSeconds;
}
