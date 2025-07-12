using Domain.Primitives;

namespace Modules.Users.Domain.Users;

public sealed record UserCreatedDomainEvent(
    Guid Id,
    Guid UserId,
    DateTime OccurredOnUtc
) : DomainEvent(Id, OccurredOnUtc);
