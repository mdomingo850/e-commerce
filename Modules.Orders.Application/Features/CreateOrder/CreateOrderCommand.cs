using Ardalis.Result;
using MediatR;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Features.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    HashSet<OrderItemModel> OrderItems) : IRequest<Result>
{
}
