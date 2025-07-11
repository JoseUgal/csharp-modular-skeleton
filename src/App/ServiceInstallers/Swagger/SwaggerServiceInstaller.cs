using Infrastructure.Configuration;

namespace App.ServiceInstallers.Swagger;

/// <summary>
/// Represents the swagger service installer.
/// </summary>
internal sealed class SwaggerServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
