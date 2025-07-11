using Microsoft.Extensions.DependencyInjection;

namespace Modules.Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
