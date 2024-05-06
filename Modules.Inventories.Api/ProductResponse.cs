using SharedKernel.Domain.Entities.ValueObjects;

namespace Modules.Inventories.Api;

public sealed record ProductResponse(Guid Id, string Name, Money Price, int Quantity)
{
}
