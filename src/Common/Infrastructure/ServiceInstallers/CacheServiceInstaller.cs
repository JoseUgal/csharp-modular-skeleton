using Application.Caching;
using Infrastructure.Caching;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the cache service installer.
/// </summary>
internal sealed class CacheServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();
    }
}
