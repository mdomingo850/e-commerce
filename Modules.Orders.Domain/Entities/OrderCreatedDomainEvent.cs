using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Orders.Domain.Entities;

public record OrderCreatedDomainEvent : IDomainEvent
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }

    public OrderCreatedDomainEvent(Guid id, Guid orderId)
    {
        Id = id;
        OrderId = orderId;
    }
}
