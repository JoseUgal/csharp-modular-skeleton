using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Users;

namespace Infrastructure.Database;

/// <summary>
/// Represents the application database context.
/// </summary>
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    /// <summary>
    /// Access to the users.
    /// </summary>
    public DbSet<User> Users { get; set; }
}
