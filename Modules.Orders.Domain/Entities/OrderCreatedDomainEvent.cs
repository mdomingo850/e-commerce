using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Orders.Domain.Entities;

public record OrderCreatedDomainEvent(Guid Id, Guid OrderId, Guid ProductId, int QuantityBought) : IDomainEvent
{
}
