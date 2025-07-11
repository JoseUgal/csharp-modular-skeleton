using Domain.Primitives;

namespace Modules.Users.Domain.Users;

public sealed record UserCreatedDomainEvent(
    Guid UserId
) : IDomainEvent;
