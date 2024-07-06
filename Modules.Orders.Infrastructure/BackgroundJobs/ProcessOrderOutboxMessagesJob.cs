using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Orders.Persistence;
using Modules.Orders.Persistence.Models;
using Newtonsoft.Json;
using Quartz;
using SharedKernel.Domain.Entities.Primitives;
using SharedKernel.Models;
using System;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace Modules.Orders.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly OrderDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IEnvironmentVariables _environmentVariables;

    public ProcessOutboxMessagesJob(
        OrderDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessOutboxMessagesJob> logger,
        IEnvironmentVariables environmentVariables)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
        _environmentVariables = environmentVariables;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await GetAndLockRecords(context);

        if (!messages.Any())
        {
            //_logger.LogInformation("No messages to process");
            return;
        }

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            if (domainEvent is null)
            {
                //Add logging
                continue;
            }

            await _publisher.Publish(domainEvent, context.CancellationToken);

            message.ProcessedOn = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            //_logger.LogInformation($"Message {message.Id} is processed");
        }
    }

    private async Task<IEnumerable<OutboxMessage>> GetAndLockRecords(IJobExecutionContext context)
    {
        IEnumerable<OutboxMessage> messages = null;
        for (int retryCount = 0; retryCount < 3; retryCount++) // Adjust retry count as needed
        {
            messages = await _dbContext.Set<OutboxMessage>().Where(m => m.ProcessedOn == null && !m.IsLocked)
            .Take(10).ToListAsync(context.CancellationToken);

            _logger.LogInformation("{apiName} - Messages fetched for retry {retry}", _environmentVariables.ApiName, retryCount + 1);

            if (!messages.Any())
            {
                //_logger.LogInformation("No messages to process for retry {retry}", retryCount + 1);
                return messages;
            }

            try
            {
                foreach (var message in messages)
                {
                    message.IsLocked = true;
                    //_logger.LogInformation("Locking message {messageId} for retry {retry}", message.Id, retryCount + 1);
                }

                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("{apiName} - Messages are locked for retry {retryCount}", _environmentVariables.ApiName, retryCount + 1);

                return messages;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("{apiName} - Concurrency Exception on attempt {retryCount}", _environmentVariables.ApiName, retryCount + 1);
                // Implement a wait strategy between retries (optional)
                foreach (var message in messages)
                {
                    _dbContext.Entry(message).Reload();
                }
                
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount))); // Exponential backoff
            }
        }

        return new List<OutboxMessage>();
    }
}
