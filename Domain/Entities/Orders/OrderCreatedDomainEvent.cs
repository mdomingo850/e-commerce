using Domain.Primitives;

namespace Domain.Entities.Orders;

public class OrderCreatedDomainEvent : DomainEvent
{
    public Guid Id { get; set; }
    public Order Order { get; set; }
    public OrderItem OrderItem { get; set; }

    public OrderCreatedDomainEvent(Guid id, Order order, OrderItem orderItem) : base(id) 
    {
        Id = id;
        Order = order;
        OrderItem = orderItem;
    }

    public override DomainEventMessage MapToMessage()
    {
        return new OrderCreatedDomainEventMessage(Id, Order.Id, OrderItem);
    }
}
