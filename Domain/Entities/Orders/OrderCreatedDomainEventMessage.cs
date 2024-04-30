using Domain.Entities.Orders;
using Domain.Primitives;

public record  OrderCreatedDomainEventMessage(Guid Id, int OrderId, OrderItem OrderItem) : DomainEventMessage(Id)
{
};