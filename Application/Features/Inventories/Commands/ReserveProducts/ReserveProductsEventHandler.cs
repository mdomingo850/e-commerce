using Application.Contracts;
using Application.Events;
using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Inventories.Commands.ReserveProducts;

internal class ReserveProductsEventHandler : INotificationHandler<OrderPaidEvent>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPublisher _mediator;
    private readonly ILogger<ReserveProductsEventHandler> _logger;

    public ReserveProductsEventHandler(
        IInventoryRepository inventoryRepository,
        IPublisher mediator,
        ILogger<ReserveProductsEventHandler> logger)
    {
        _inventoryRepository = inventoryRepository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(OrderPaidEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Reserve products started");
        
        var product = await _inventoryRepository.GetProductByIdAsync(notification.OrderItem.Product.Id);

        if (product == null) return;

        var reserveProductResult =  product.ReserveProducts(notification.OrderItem.Quanitity);


        _logger.LogInformation("Reserve products completed");

        if (!reserveProductResult.IsSuccess)
        {
            await _mediator.Publish(new ReserveProductsFailedEvent(notification.OrderId));
        }

        await _mediator.Publish(new ProductsReservedEvent(notification.OrderId));
    }
}
