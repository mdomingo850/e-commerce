using Domain.Primitives;

namespace Application.Contracts;

public interface IMessageBus
{
	Task Publish(IDomainEvent message);
}
