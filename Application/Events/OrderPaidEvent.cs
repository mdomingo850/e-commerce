using Domain.Entities.Orders;
using MediatR;

namespace Application.Events;

internal record OrderPaidEvent(int OrderId, OrderItem OrderItem) : INotification
{
}
