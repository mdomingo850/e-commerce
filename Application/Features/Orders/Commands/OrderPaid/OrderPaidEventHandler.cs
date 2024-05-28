//using Application.Contracts;
//using Application.Events;
//using Domain.Entities.Orders;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace Application.Features.Orders.Commands.OrderPaid;

//internal class OrderPaidEventHandler : INotificationHandler<OrderPaidEvent>
//{
//    private readonly IOrderRepository _orderRepository;
//    private readonly ILogger<OrderPaidEventHandler> _logger;

//    public OrderPaidEventHandler(
//        IOrderRepository orderRepository,
//        ILogger<OrderPaidEventHandler> logger)
//    {
//        _orderRepository = orderRepository;
//        _logger = logger;
//    }

//    public async Task Handle(OrderPaidEvent notification, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Order paid event handler started");
//        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

//        order.UpdateOrderStatus(OrderStatus.Paid);

//        await _orderRepository.UpdateAsync(order);
//        _logger.LogInformation("Order paid event handler completed");

//    }
//}
