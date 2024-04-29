using Application.Contracts;
using Application.Events;
using Domain.Entities.Orders;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace Application.Features.Orders.Commands.ProductsReserved;

internal class ProductsReservedEventHandler : INotificationHandler<ProductsReservedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<ProductsReservedEventHandler> _logger;

    public ProductsReservedEventHandler(IOrderRepository orderRepository, ILogger<ProductsReservedEventHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Handle(ProductsReservedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Products reserved event handler started");
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        order.UpdateOrderStatus(OrderStatus.ProductReserved);

        await _orderRepository.UpdateAsync(order);

        _logger.LogInformation("Products reserved event handler completed");
    }
}
