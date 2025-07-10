namespace App.Endpoints;

/// <summary>
/// Represents an endpoint that can be mapped to a route in the application.  This interface allows for flexible and modular endpoint registration.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Maps the endpoint to a route in the application's request pipeline.
    /// </summary>
    /// <param name="app">The IEndpointRouteBuilder used to configure the application's routes.</param>
    void MapEndpoint(IEndpointRouteBuilder app);
}

