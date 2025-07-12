using Application.Data;
using Application.Time;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Modules.Users.Persistence;
using Newtonsoft.Json;
using Persistence.Outbox;
using Polly;
using Polly.Retry;
using Quartz;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the background job for processing outbox messages.
/// </summary>
[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(
    UsersDbContext dbContext,
    IUnitOfWork unitOfWork,
    IPublisher publisher,
    IDateTimeProvider dateTimeProvider,
    IOptions<ProcessOutboxMessagesOptions> options
) : IJob
{
    private readonly AsyncRetryPolicy _policy = Policy.Handle<Exception>().RetryAsync(options.Value.RetryCount);

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    /// <inheritdoc />
    public async Task Execute(IJobExecutionContext context)
    {
        var outboxMessages = await GetOutboxMessagesAsync();

        if (outboxMessages.Count == 0)
        {
            return;
        }

        foreach (var outboxMessage in outboxMessages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                outboxMessage.Content,
                JsonSerializerSettings
            )!;

            var result = await _policy.ExecuteAndCaptureAsync(() =>
                publisher.Publish(domainEvent, context.CancellationToken)
            );

            await UpdateOutboxMessageAsync(outboxMessage, result.FinalException);
        }
    }

    private async Task<List<OutboxMessage>> GetOutboxMessagesAsync()
    {
        var outboxMessages = await dbContext.Set<OutboxMessage>()
            .Where(outboxMessage => outboxMessage.ProcessedOnUtc == null)
            .OrderBy(outboxMessage => outboxMessage.OccurredOnUtc)
            .Take(options.Value.BatchSize)
            .ToListAsync();

        return outboxMessages;
    }

    private async Task UpdateOutboxMessageAsync(OutboxMessage outboxMessage, Exception? exception)
    {
        outboxMessage.SetProcessed(dateTimeProvider.UtcNow, exception?.ToString());

        dbContext.Set<OutboxMessage>().Update(outboxMessage);

        await unitOfWork.SaveChangesAsync();
    }

}
