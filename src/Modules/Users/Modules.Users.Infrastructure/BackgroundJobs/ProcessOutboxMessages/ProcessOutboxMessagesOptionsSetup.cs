using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the <see cref="ProcessOutboxMessagesOptions"/> setup.
/// </summary>
internal sealed class ProcessOutboxMessagesOptionsSetup(IConfiguration configuration) : IConfigureOptions<ProcessOutboxMessagesOptions>
{
    private const string ConfigurationSectionName = "Modules:Users:BackgroundJobs:ProcessOutboxMessages";

    /// <inheritdoc />
    public void Configure(ProcessOutboxMessagesOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
}
