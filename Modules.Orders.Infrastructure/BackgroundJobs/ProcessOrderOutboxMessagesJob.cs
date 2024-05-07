using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Persistence;
using Modules.Orders.Persistence.Models;
using Newtonsoft.Json;
using Quartz;
using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Orders.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly OrderDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(
        OrderDbContext dbContext,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext.Set<OutboxMessage>().Where(m => m.ProcessedOn == null)
            .Take(20).ToListAsync(context.CancellationToken);

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
        }

        await _dbContext.SaveChangesAsync();
    }
}
