using Application.Data;
using Application.Time;
using Domain.Primitives;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessOutboxMessages;

/// <summary>
/// Represents the background job for processing outbox messages.
/// </summary>
[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(
    ISqlQueryExecutor sqlQueryExecutor,
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

    private async Task<List<OutboxMessageResponse>> GetOutboxMessagesAsync()
    {
        var sql = $"""
                       SELECT id, content
                       FROM users.outbox_messages
                       WHERE processed_on_utc IS NULL
                       ORDER BY occurred_on_utc
                       LIMIT {options.Value.BatchSize}
                   """;

        var outboxMessages = await sqlQueryExecutor.QueryAsync<OutboxMessageResponse>(sql);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(OutboxMessageResponse outboxMessage, Exception? exception)
    {
        const string sql = """
                               UPDATE users.outbox_messages
                               SET processed_on_utc = @ProcessedOnUtc,
                                   error = @Error
                               WHERE id = @Id
                           """;

        await sqlQueryExecutor.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            }
        );
    }

    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
