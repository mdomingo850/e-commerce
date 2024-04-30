using Ardalis.Result;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    HashSet<Tuple<Guid, int>> OrderItems) : IRequest<Result>
{ 
}
