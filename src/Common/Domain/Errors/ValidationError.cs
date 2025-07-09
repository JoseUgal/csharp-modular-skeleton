using Domain.Results;

namespace Domain.Errors;

/// <summary>
/// Represents the validation result containing an array of errors.
/// </summary>
public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors) : base("Validation.General", "One or more validation errors occurred", ErrorType.Validation)
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets the errors.
    /// </summary>
    public Error[] Errors { get; }

    /// <summary>
    /// Initialize validation error from list of results.
    /// </summary>
    public static ValidationError FromResults(IEnumerable<Result> results) => new(
        results
            .Where(r => r.IsFailure)
            .Select(r => r.Error)
            .ToArray()
    );
}
