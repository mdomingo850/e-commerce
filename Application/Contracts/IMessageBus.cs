using SharedKernel.Domain.Entities.Primitives;

namespace Application.Contracts;

public interface IMessageBus
{
	Task Publish(IDomainEvent message);
}
