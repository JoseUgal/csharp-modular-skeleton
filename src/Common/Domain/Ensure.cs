using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain;

public static class Ensure
{
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = null
    )
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }

    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression("value")] string? paramName = null
    )
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }
}
