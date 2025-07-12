using Endpoints;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Users.Application.Users.Create;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Users;

/// <summary>
/// Represents the endpoint for registering a new user.
/// </summary>
internal sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(UserRoutes.Register, async ([FromBody] RegisterUserRequest request, ISender sender) =>
            {
                var command = new CreateUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName
                );

                var result = await sender.Send(command);

                return result.Match(Results.Ok, CustomResults.Problem);
            }
        ).WithTags(UserRoutes.Tag);
    }

    /// <summary>
    /// Represents the request for registering a new user.
    /// </summary>
    /// <param name="Email">The email.</param>
    /// <param name="FirstName">The first name.</param>
    /// <param name="LastName">The last name.</param>
    public sealed record RegisterUserRequest(
        string Email,
        string FirstName,
        string LastName
    );
}

