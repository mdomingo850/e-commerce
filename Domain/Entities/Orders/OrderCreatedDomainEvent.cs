using Domain.Primitives;

namespace Domain.Entities.Orders;

public record OrderCreatedDomainEvent : IDomainEvent
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderItem OrderItem { get; set; }

    public OrderCreatedDomainEvent(Guid id, Guid orderId, OrderItem orderItem)
    {
        Id = id;
        OrderId = orderId;
        OrderItem = orderItem;
    }
}
