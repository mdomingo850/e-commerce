using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Domain.Entities;
using Modules.Inventories.Persistence;
using Newtonsoft.Json;
using Quartz;
using SharedKernel.Domain.Entities.Primitives;
using SharedKernel.Models;
using SharedKernel.Persistence;

namespace Modules.Orders.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessInventoryInboxMessagesJob : IJob
{
    private readonly InventoryDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessInventoryInboxMessagesJob> _logger;
    private readonly IEnvironmentVariables _environmentVariables;

    public ProcessInventoryInboxMessagesJob(
        InventoryDbContext dbContext,
        IPublisher publisher,
        ILogger<ProcessInventoryInboxMessagesJob> logger,
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
            var integrationEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            if (integrationEvent is null)
            {
                //Add logging
                continue;
            }

            await _publisher.Publish(integrationEvent, context.CancellationToken);

            message.ProcessedOn = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            //_logger.LogInformation($"Message {message.Id} is processed");
        }
    }

    private async Task<IEnumerable<InboxMessage>> GetAndLockRecords(IJobExecutionContext context)
    {
        IEnumerable<InboxMessage> messages = null;
        for (int retryCount = 0; retryCount < 3; retryCount++) // Adjust retry count as needed
        {
            messages = await _dbContext.Set<InboxMessage>().Where(m => m.ProcessedOn == null && !m.IsLocked)
            .Take(10).ToListAsync(context.CancellationToken);

            _logger.LogInformation("{apiName} - Inventory Inbox Messages fetched for retry {retry}", 
                _environmentVariables.ApiName, retryCount + 1);

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
                _logger.LogInformation("{apiName} - Messages are locked for retry {retryCount}", 
                    _environmentVariables.ApiName, retryCount + 1);

                return messages;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("{apiName} - Concurrency Exception on attempt {retryCount}", 
                    _environmentVariables.ApiName, retryCount + 1);

                // Implement a wait strategy between retries (optional)
                foreach (var message in messages)
                {
                    _dbContext.Entry(message).Reload();
                }
                
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount))); // Exponential backoff
            }
        }

        return new List<InboxMessage>();
    }
}
