using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence;
using Persistence.Models;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly SharedDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(
        SharedDbContext dbContext,
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
