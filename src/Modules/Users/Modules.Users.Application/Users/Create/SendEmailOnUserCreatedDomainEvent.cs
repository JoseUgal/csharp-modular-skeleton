using Application.Messaging;
using Modules.Users.Domain.Users;

namespace Modules.Users.Application.Users.Create;

/// <summary>
/// Represents the <see cref="UserCreatedDomainEvent"/> handler.
/// Sends a welcome or confirmation email when a new user is created.
/// </summary>
internal sealed class SendEmailOnUserCreatedDomainEvent : IDomainEventHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // TODO: Send email

        return Task.CompletedTask;
    }
}
