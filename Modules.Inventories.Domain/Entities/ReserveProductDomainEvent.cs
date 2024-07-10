using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Inventories.Domain.Entities;

public record ReserveProductDomainEvent(Guid OrderId, Guid ProductId, int QuantityBought) : IDomainEvent
{
}
