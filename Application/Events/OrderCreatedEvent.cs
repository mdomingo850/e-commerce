using Domain.Entities.Orders;
using MediatR;

namespace Application.Events;

internal record OrderCreatedEvent(Guid OrderId, OrderItem OrderItem) : INotification
{
}
