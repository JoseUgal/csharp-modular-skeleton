namespace Domain.Errors;

/// <summary>
/// Represents an error.
/// </summary>
public record Error
{
    /// <summary>
    /// The empty error instance.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// The null value error instance.
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", ErrorType.Failure);

    /// <summary>
    /// The condition not met error instance.
    /// </summary>
    public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.", ErrorType.Failure);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    /// <param name="type">The error type.</param>
    public Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the error type.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with failure type.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with not found type.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with problem type.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Problem(string code, string description) => new(code, description, ErrorType.Problem);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with conflict type.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="description">The error description.</param>
    public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
}
