namespace Domain.Primitives;

public abstract class DomainEvent(Guid Id)
{
    public abstract DomainEventMessage MapToMessage();
}
