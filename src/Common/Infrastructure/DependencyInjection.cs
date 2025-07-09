using Application.Data;
using Application.Time;
using Domain;
using Infrastructure.Database;
using Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        Ensure.NotNullOrEmpty(connectionString);

        services.AddDbContext<ApplicationDbContext>((_, options) =>
            {
                options.UseNpgsql(connectionString);

                options.UseUpperSnakeCaseNamingConvention();
            }
        );

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
