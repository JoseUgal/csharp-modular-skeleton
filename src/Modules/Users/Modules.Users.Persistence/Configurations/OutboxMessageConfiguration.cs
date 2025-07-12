using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Users;
using Modules.Users.Persistence.Constants;
using Persistence.Outbox;

namespace Modules.Users.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="OutboxMessage"/> entity configuration.
/// </summary>
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<OutboxMessage> builder) => builder.Tap(ConfigureDataStructure);

    private static void ConfigureDataStructure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.OccurredOnUtc).IsRequired();

        builder.Property(x => x.Type).IsRequired();

        builder.Property(x => x.Content).HasColumnType("json").IsRequired();

        builder.Property(x => x.ProcessedOnUtc).IsRequired(false);

        builder.Property(x => x.Error).IsRequired(false);
    }

}
