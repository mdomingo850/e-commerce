using Application.Contracts;
using Application.Events;
using Ardalis.Result;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.Orders.Commands.OrderCreated;

internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly INotificationService _notificationService;

    public OrderCreatedEventHandler(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IPaymentService paymentService,
        IInventoryRepository inventoryRepository,
        INotificationService notification)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _inventoryRepository = inventoryRepository;
        _notificationService = notification;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        if (order is null)
        {
            return;
        }

        //send confirmation email
        var sendNotificationResult = await _notificationService.SendAsync();

        //payment processing
        var payResult = await _paymentService.PayAsync();

        if (!payResult.IsSuccess)
        {
            order.UpdateOrderStatus(OrderStatus.PaymentFailed);
            await _orderRepository.UpdateAsync(order);
            return;
        }

        var firstOrderItem = order.OrderItems.First();
        var product = firstOrderItem.Product;
        var firstProductQuanitity = firstOrderItem.Quanitity;

        //inventory stock reduction
        var reserveProductsResult = product.ReserveProducts(firstProductQuanitity);

        //roll back payment processing
        if (!reserveProductsResult.IsSuccess)
        {
            var undoPaymentResult = await _paymentService.UndoPaymentAsync();

            if (!undoPaymentResult.IsSuccess)
            {
                //handle undo payment error logic
            }

            order.UpdateOrderStatus(OrderStatus.InventoryReserveFailed);
            await _orderRepository.UpdateAsync(order);
            return;
        }

        order.UpdateOrderStatus(OrderStatus.ProductReserved);
        await _orderRepository.UpdateAsync(order);
    }
}
