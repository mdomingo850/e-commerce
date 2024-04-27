using Application.Contracts;
using Application.Events;
using Domain.Entities.Orders;

namespace Application.Features.Orders.Commands.PaymentFailed;

internal class PaymentFailedEventHandler
{
    private readonly IOrderRepository _orderRepository;

    public PaymentFailedEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(OrderPaymentFailedEvent message)
    {
        var order = await _orderRepository.GetByIdAsync(message.OrderId);

        order.UpdateOrderStatus(OrderStatus.PaymentFailed);

        await _orderRepository.UpdateAsync(order);

        return;
    }
}
