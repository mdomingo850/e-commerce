using Application.Contracts;
using Application.Events;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.OrderPaid;

internal class OrderPaidEventHandler : INotificationHandler<OrderPaidEvent>
{
    private readonly IOrderRepository _orderRepository;

    public OrderPaidEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderPaidEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        order.UpdateOrderStatus(OrderStatus.Paid);

        await _orderRepository.UpdateAsync(order);
    }
}
