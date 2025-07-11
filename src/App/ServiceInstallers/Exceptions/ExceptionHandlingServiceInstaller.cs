using App.Infrastructure;
using Infrastructure.Configuration;

namespace App.ServiceInstallers.Exceptions;

/// <summary>
/// Represents the startup tasks service installer.
/// </summary>
internal sealed class ExceptionHandlingServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
    }
}
