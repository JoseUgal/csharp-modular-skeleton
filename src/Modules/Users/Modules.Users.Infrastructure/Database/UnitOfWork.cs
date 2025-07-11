using Application.Data;
using Application.ServiceLifetimes;

namespace Modules.Users.Infrastructure.Database;

/// <summary>
/// Represents the user's module unit of work.
/// </summary>
internal sealed class UnitOfWork(UsersDbContext context) : IUnitOfWork, IScoped
{
    /// <inheritdoc />
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await context.SaveChangesAsync(cancellationToken);
}
