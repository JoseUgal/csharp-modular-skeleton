using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Persistence.Extensions;

/// <summary>
/// Contains extension method for the <see cref="NpgsqlDbContextOptionsBuilder"/> class.
/// </summary>
public static class NpgsqlDbContextOptionsBuilderExtensions
{
    /// <summary>
    /// The migration history table.
    /// </summary>
    private const string MigrationHistory = "__EFMigrationsHistory";

    /// <summary>
    /// Configures the migration history table to live in the specified schema.
    /// </summary>
    /// <param name="dbContextOptionsBuilder">The database context options builder.</param>
    /// <param name="schema">The schema.</param>
    /// <returns>The same database context options builder.</returns>
    public static NpgsqlDbContextOptionsBuilder WithMigrationHistoryTableInSchema(
        this NpgsqlDbContextOptionsBuilder dbContextOptionsBuilder,
        string schema
    ) => dbContextOptionsBuilder.MigrationsHistoryTable(MigrationHistory, schema);
}
