using System.Reflection;
using App.Endpoints;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace App.Extensions;

/// <summary>
/// Provides extension methods for registering and mapping endpoints in the application.
/// </summary>
public static class EndpointExtensions
{
    /// <summary>
    /// Registers endpoints from a specified assembly into the dependency injection container.  Only concrete types implementing IEndpoint are registered.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="assembly">The assembly containing the endpoint implementations.</param>
    /// <returns>The configured IServiceCollection.</returns>
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    /// <summary>
    /// Maps all registered endpoints to routes in the application's request pipeline.
    /// </summary>
    /// <param name="app">The WebApplication to configure.</param>
    /// <param name="routeGroupBuilder">Optional RouteGroupBuilder for grouping endpoints.</param>
    /// <returns>The configured WebApplication.</returns>
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
}
