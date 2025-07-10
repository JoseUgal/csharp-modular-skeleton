using Domain.Results;

namespace App.Extensions;

/// <summary>
/// Provides extension methods for working with Result objects, enabling pattern matching based on success or failure.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Executes a specified action if the result is successful, or another action if it fails.
    /// </summary>
    /// <param name="result">The Result object to evaluate.</param>
    /// <param name="onSuccess">Action to execute if the result is successful.</param>
    /// <param name="onFailure">Action to execute if the result fails.  Receives the original Result object.</param>
    /// <returns>The result of the onSuccess or onFailure action.</returns>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    /// Executes a specified action if the result is successful, or another action if it fails.  When failing, receives the original Result object containing a value.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in a successful Result.</typeparam>
    /// <typeparam name="TOut">The type of the result to return.</typeparam>
    /// <param name="result">The Result object to evaluate.</param>
    /// <param name="onSuccess">Action to execute if the result is successful. Receives the value from a successful Result.</param>
    /// <param name="onFailure">Action to execute if the result fails. Receives the original Result object.</param>
    /// <returns>The result of the onSuccess or onFailure action.</returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
