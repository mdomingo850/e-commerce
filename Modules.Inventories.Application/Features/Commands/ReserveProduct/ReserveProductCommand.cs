using MediatR;

namespace Modules.Inventories.Application.Features.Commands.ReserveProduct;

public sealed record ReserveProductCommand(Guid OrderId
    , Guid ProductId, int QuantityBought
    ) : IRequest
{
}
