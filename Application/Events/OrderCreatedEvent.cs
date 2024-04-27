using Domain.Entities.Orders;
using MediatR;

namespace Application.Events;

internal record OrderCreatedEvent(int OrderId, OrderItem OrderItem) : INotification
{
}
