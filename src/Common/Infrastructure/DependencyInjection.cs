using Application.Caching;
using Application.Data;
using Application.Time;
using Infrastructure.Caching;
using Infrastructure.Database;
using Infrastructure.Database.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Domain.Users;

namespace Infrastructure;

/// <summary>
/// Configures infrastructure-specific services using dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The configured IServiceCollection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddPersistence(configuration);

        services.AddCaching();

        services.AddRepositories();

        return services;
    }

    /// <summary>
    /// Configures database-related services.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The configured IServiceCollection.</returns>
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(
                connectionString,
                "The database connection string is required."
            );
        }

        services.TryAddSingleton<UpdateAuditableEntitiesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);

                options.UseUpperSnakeCaseNamingConvention();

                options.AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>()
                );
            }
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Configures caching-related services.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <returns>The configured IServiceCollection.</returns>
    private static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }

    /// <summary>
    /// Configures repositories from different modules
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
