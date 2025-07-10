using Application.Time;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Database.Interceptors;

/// <summary>
/// Represents the interceptor for updating auditable entity values.
/// </summary>
public sealed class UpdateAuditableEntitiesInterceptor(IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var utcNow = dateTimeProvider.UtcNow;

        foreach (var auditable in GetAuditableEntities(eventData.Context))
        {
            if (auditable.State == EntityState.Added)
            {
                auditable.Property(nameof(IAuditable.CreatedOnUtc)).CurrentValue = utcNow;
            }

            if (auditable.State == EntityState.Modified)
            {
                auditable.Property(nameof(IAuditable.ModifiedOnUtc)).CurrentValue = utcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static IEnumerable<EntityEntry<IAuditable>> GetAuditableEntities(DbContext dbContext) => dbContext.ChangeTracker.Entries<IAuditable>();
}
