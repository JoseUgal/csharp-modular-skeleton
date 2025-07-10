using Application.Time;

namespace Infrastructure.Time;


/// <inheritdoc />
internal sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}
