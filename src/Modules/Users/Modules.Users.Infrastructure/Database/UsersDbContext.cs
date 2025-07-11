using Microsoft.EntityFrameworkCore;
using Modules.Users.Infrastructure.Database.Constants;

namespace Modules.Users.Infrastructure.Database;

/// <summary>
/// Represents the users module database context.
/// </summary>
public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
