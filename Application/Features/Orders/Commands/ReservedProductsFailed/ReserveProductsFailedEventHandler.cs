using Application.Contracts;
using Application.Events;
using Domain.Entities.Orders;

namespace Application.Features.Orders.Commands.ReservedProductsFailed;

internal class ReserveProductsFailedEventHandler
{
    private readonly IOrderRepository _orderRepository;

    public ReserveProductsFailedEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(ReserveProductsFailedEvent message)
    {
        var order = await _orderRepository.GetByIdAsync(message.OrderId);

        order.UpdateOrderStatus(OrderStatus.InventoryReserveFailed);

        await _orderRepository.UpdateAsync(order);
    }
}
