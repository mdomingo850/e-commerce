using Ardalis.Result;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    int CustomerId,
    HashSet<Tuple<int, int>> OrderItems) : IRequest<Result>
{ 
}
