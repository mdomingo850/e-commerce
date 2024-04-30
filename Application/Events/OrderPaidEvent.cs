using Domain.Entities.Orders;
using MediatR;

namespace Application.Events;

internal record OrderPaidEvent(Guid OrderId, OrderItem OrderItem) : INotification
{
}
