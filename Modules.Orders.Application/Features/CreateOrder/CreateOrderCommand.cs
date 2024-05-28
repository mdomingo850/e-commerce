using Ardalis.Result;
using MediatR;

namespace Modules.Orders.Application.Features.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    HashSet<Tuple<Guid, int>> OrderItems) : IRequest<Result>
{
}
