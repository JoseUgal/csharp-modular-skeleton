using MediatR;

namespace Domain.Primitives;

/// <summary>
/// Represents the domain event interface.
/// </summary>
public interface IDomainEvent : INotification;
