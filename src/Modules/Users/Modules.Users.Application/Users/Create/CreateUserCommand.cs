using Application.Messaging;

namespace Modules.Users.Application.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string FirstName,
    string LastName
) : ICommand<Guid>;
