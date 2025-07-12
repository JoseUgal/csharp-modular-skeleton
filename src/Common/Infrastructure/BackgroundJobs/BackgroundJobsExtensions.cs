using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Infrastructure.BackgroundJobs;

/// <summary>
/// Contains extension of background jobs methods for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class BackgroundJobsExtensions
{
    /// <summary>
    /// Adds all the implementations of <see cref="IRecurringJobConfiguration"/> inside the specified assembly as scoped.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="assembly">The assembly to scan for scoped services.</param>
    /// <returns>The same service collection so that multiple calls can be chained.</returns>
    public static void AddRecurringJobConfigurations(this IServiceCollection services, Assembly assembly) =>
        services.Scan(scan =>
            scan.FromAssemblies(assembly)
                .AddClasses(filter => filter.Where(type => type.IsAssignableTo(typeof(IRecurringJobConfiguration))), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );
}
