using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Orders.Domain.Entities;
using Modules.Orders.IntegrationEvents;

namespace Modules.Orders.Application.Features.OrderCreated;

internal class OrderCreatedEventHandlerEventDriven : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OrderCreatedEventHandlerEventDriven> _logger;

    public OrderCreatedEventHandlerEventDriven(
        IPublishEndpoint publishEndpoint, 
        ILogger<OrderCreatedEventHandlerEventDriven> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"OrderCreatedDomainEvent started - {notification.OrderId}");

        await _publishEndpoint.Publish(new OrderCreatedIntegrationEvent(
            notification.OrderId, 
            notification.ProductId, 
            notification.QuantityBought));
    }
}
