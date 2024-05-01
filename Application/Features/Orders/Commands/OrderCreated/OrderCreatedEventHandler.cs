using Application.Contracts;
using Application.Events;
using Ardalis.Result;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using MediatR;

namespace Application.Features.Orders.Commands.OrderCreated;

internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;

    public OrderCreatedEventHandler(
        IOrderRepository orderRepository,
        IPaymentService paymentService)
    {
        _orderRepository = orderRepository;
        _paymentService = paymentService;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        if (order is null)
        {
            return;
        }

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
