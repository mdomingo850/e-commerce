using MediatR;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;
using Modules.Payments.Api;

namespace Modules.Orders.Application.Features.OrderCreated;

internal class OrderCreatedEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentsApi _paymentsApi;

    public OrderCreatedEventHandler(
        IOrderRepository orderRepository,
        IPaymentsApi paymentsApi)
    {
        _orderRepository = orderRepository;
        _paymentsApi = paymentsApi;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(notification.OrderId);

        if (order is null)
        {
            return;
        }

        //payment processing
        var payResult = await _paymentsApi.PayAsync();

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
            var undoPaymentResult = await _paymentsApi.UndoPaymentAsync();

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
