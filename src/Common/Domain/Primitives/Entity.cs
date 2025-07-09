namespace Domain.Primitives;

/// <summary>
/// Represents the abstract entity primitive.
/// </summary>
public abstract class Entity
{
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
