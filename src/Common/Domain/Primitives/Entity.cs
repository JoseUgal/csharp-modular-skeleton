namespace Domain.Primitives;

/// <summary>
/// Represents the abstract entity primitive.
/// </summary>
public abstract class Entity
{
    private List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events and clears the list.
    /// </summary>
    /// <returns>The domain events.</returns>
    public IReadOnlyList<IDomainEvent> PullDomainEvents()
    {
        var domainEvents = _domainEvents.ToList();

        _domainEvents = [];

        return domainEvents;
    }

    /// <summary>
    /// Add the specified domain event.
    /// </summary>
    /// <param name="domainEvent">The domain event.</param>
    protected void Record(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required for deserialization.
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    public Guid Id { get; init; }
}
