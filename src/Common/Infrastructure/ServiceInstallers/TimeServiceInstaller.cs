using Application.Time;
using Infrastructure.Configuration;
using Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the time service installer.
/// </summary>
internal sealed class TimeServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    }
}
