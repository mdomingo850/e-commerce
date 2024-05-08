using Application.Contracts;
using Domain.Primitives;

namespace Infrastructure.Services;

public class RabbitMQ : IMessageBus
{
    public Task Publish(IDomainEvent message)
    {
        throw new NotImplementedException();
    }
}
