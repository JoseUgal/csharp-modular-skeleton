namespace Persistence.Outbox;

/// <summary>
/// Represents the outbox message.
/// </summary>
public sealed class OutboxMessage
{
    /// <summary>
    /// Marks the outbox message as processed and sets the optional error message.
    /// </summary>
    /// <param name="processedOn">The UTC date and time when the message was processed.</param>
    /// <param name="error">The error message, if processing failed.</param>
    public void SetProcessed(DateTime processedOn, string? error)
    {
        ProcessedOnUtc = processedOn;
        Error = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OutboxMessage"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="occurredOnUtc">The occurred on date and time.</param>
    /// <param name="type">The type.</param>
    /// <param name="content">The content.</param>
    public OutboxMessage(Guid id, DateTime occurredOnUtc, string type, string content)
    {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
        Content = content;
        Type = type;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the occurred on date and time.
    /// </summary>
    public DateTime OccurredOnUtc { get; private set; }

    /// <summary>
    /// Gets the type.
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    public string Content { get; private set; }

    /// <summary>
    /// Gets the processed on date and time, if it exists.
    /// </summary>
    public DateTime? ProcessedOnUtc { get; private set; }

    /// <summary>
    /// Gets the error, if it exists.
    /// </summary>
    public string? Error { get; private set; }
}
