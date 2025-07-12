using Endpoints;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Modules.Users.Application.Users.Create;
using Modules.Users.Endpoints.Routes;

namespace Modules.Users.Endpoints.Users.Register;

/// <summary>
/// Represents the endpoint for registering a new user.
/// </summary>
internal sealed class RegisterUserEndpoint : IEndpoint
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
            )
            .WithTags(UserRoutes.Tag)
            .WithName(nameof(RegisterUserEndpoint))
            .WithSummary("Register a new user.")
            .WithDescription("This endpoint able to register a new user using the email address, first and last name.")
            .Accepts<RegisterUserRequest>("application/json")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithOpenApi(operation =>
                {
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content =
                        {
                            ["application/json"] = new OpenApiMediaType
                            {
                                Example = new OpenApiObject
                                {
                                    ["email"] = new OpenApiString("example@example.com"),
                                    ["firstName"] = new OpenApiString("Juan"),
                                    ["lastName"] = new OpenApiString("Lord"),
                                }
                            }
                        }
                    };

                    return operation;
                }
            );
    }
}

