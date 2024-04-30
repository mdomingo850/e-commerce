namespace Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<DomainEvent> _domainEvents = new();
    protected AggregateRoot(int Id) : base(Id)
    {
    }

    public IReadOnlyCollection<DomainEventMessage> GetDomainEvents() => _domainEvents.Select(x => x.MapToMessage()).ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();
    
    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
