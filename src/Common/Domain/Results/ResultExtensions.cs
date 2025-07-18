using Domain.Errors;

namespace Domain.Results;

/// <summary>
/// Contains extension methods for working with the <see cref="Result"/> class.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Maps the success result based on the specified mapping function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The mapping function.</param>
    /// <returns>The mapped result.</returns>
    public static Result<TOut> Map<TOut>(this Result result, Func<TOut> func)
    {
        if (result.IsSuccess)
        {
            return func();
        }

        return Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    /// Maps the success result based on the specified mapping function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The mapping function.</param>
    /// <returns>The mapped result.</returns>
    public static async Task<Result<TOut>> Map<TOut>(this Task<Result> resultTask, Func<TOut> func)
    {
        var result = await resultTask;

        return result.Map(func);
    }

    /// <summary>
    /// Maps the success result based on the specified mapping function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The mapping function.</param>
    /// <returns>The mapped result.</returns>
    public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func)
    {
        if (result.IsFailure)
        {
            return func(result.Value);
        }

        return Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    /// Maps the success result based on the specified mapping function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The mapping function.</param>
    /// <returns>The mapped result.</returns>
    public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> func)
    {
        var result = await resultTask;

        return result.Map(func);
    }

    /// <summary>
    /// Maps the failure result based on the specified error function, otherwise returns a success result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The error function.</param>
    /// <returns>The mapped result.</returns>
    public static Result<TIn> MapFailure<TIn>(this Result<TIn> result, Func<Error> func)
    {
        if (result.IsFailure)
        {
            var error = func();

            return Result.Failure<TIn>(error);
        }

        return result;
    }

    /// <summary>
    /// Maps the failure result based on the specified error function, otherwise returns a success result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The error function.</param>
    /// <returns>The mapped result.</returns>
    public static async Task<Result<TIn>> MapFailure<TIn>(this Task<Result<TIn>> resultTask, Func<Error> func)
    {
        var result = await resultTask;

        return result.MapFailure(func);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static Result Bind<TIn>(this Result<TIn> result, Func<TIn, Result> func)
    {
        if (result.IsFailure)
        {
            return func(result.Value);
        }

        return Result.Failure(result.Error);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> resultTask, Func<TIn, Result> func)
    {
        var result = await resultTask;

        return result.Bind(func);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func)
    {
        if (result.IsSuccess)
        {
            return await func(result.Value);
        }

        return Result.Failure(result.Error);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func)
    {
        if (result.IsSuccess)
        {
            return await func(result.Value);
        }

        return Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result<TOut>> Bind<TOut>(this Result result, Func<Task<Result<TOut>>> func)
    {
        if (result.IsSuccess)
        {
            return await func();
        }

        return Result.Failure<TOut>(result.Error);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Result<TOut>> func)
    {
        var result = await resultTask;

        return result.Bind(func);
    }

    /// <summary>
    /// Binds the success result based on the specified binding function, otherwise returns a failure result.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The binding function.</param>
    /// <returns>The bound result.</returns>
    public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, Task<Result<TOut>>> func)
    {
        var result = await resultTask;

        return await result.Bind(func);
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="func">The function.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result> Tap(this Result result, Func<Task> func)
    {
        if (result.IsSuccess)
        {
            await func();
        }

        return result;
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The function.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result> Tap(this Task<Result> resultTask, Func<Task> func)
    {
        var result = await resultTask;

        return await result.Tap(func);
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="action">The action.</param>
    /// <returns>The same result.</returns>
    public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value);
        }

        return result;
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="func">The function.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Func<Task> func)
    {
        var result = await resultTask;

        return await result.Tap(func);
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="action">The action.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Action<TIn> action)
    {
        var result = await resultTask;

        return result.Tap(action);
    }

    /// <summary>
    /// Tap will execute the provided action if the result is successful.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The function.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result, Func<Task> func)
    {
        if (result.IsSuccess)
        {
            await func();
        }

        return result;
    }

    /// <summary>
    /// On failure will execute the provided action if the result is a failure.
    /// </summary>
    /// <param name="resultTask">The result task.</param>
    /// <param name="action">The action.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<Error> action)
    {
        var result = await resultTask;

        if (result.IsFailure)
        {
            action(result.Error);
        }

        return result;
    }

    /// <summary>
    /// On failure will execute the provided action if the result is a failure.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="action">The action.</param>
    /// <returns>The same result.</returns>
    public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask, Action<Error> action)
    {
        var result = await resultTask;

        if (result.IsFailure)
        {
            action(result.Error);
        }

        return result;
    }

    /// <summary>
    /// Filter will return the success result if the specified predicate evaluates to true.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>The same result if the specified predicate evaluates to true.</returns>
    public static Result<TIn> Filter<TIn>(this Result<TIn> result, Func<TIn, bool> predicate)
    {
        if (result.IsFailure)
        {
            return result;
        }

        var evaluation = predicate(result.Value);

        if (!evaluation)
        {
            return Result.Failure<TIn>(Error.ConditionNotMet);
        }

        return result;
    }
}
