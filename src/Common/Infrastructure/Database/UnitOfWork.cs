using Application.Data;

namespace Infrastructure.Database;

/// <summary>
/// Represents the user's module unit of work.
/// </summary>
internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);
}
