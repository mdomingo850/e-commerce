using Application.Contracts;
using Application.Events;
using Domain.Entities.Orders;
using MediatR;
using System.Runtime.CompilerServices;

namespace Application.Features.Orders.Commands.ProductsReserved;

internal class ProductsReservedEventHandler : INotificationHandler<ProductsReservedEvent>
{
    private readonly IOrderRepository _orderRepository;

    public ProductsReservedEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(ProductsReservedEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        order.UpdateOrderStatus(OrderStatus.ProductReserved);

        await _orderRepository.UpdateAsync(order);
    }
}
