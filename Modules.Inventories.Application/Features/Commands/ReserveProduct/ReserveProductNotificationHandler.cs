using MassTransit;
using MediatR;
using Modules.Inventories.Domain.Entities;
using Modules.Inventories.IntegrationEvents;
using System;

namespace Modules.Inventories.Application.Features.Commands.ReserveProduct;

internal class ReserveProductNotificationHandler : INotificationHandler<ReserveProductDomainEvent>
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;

    public ReserveProductNotificationHandler(
               IMediator mediator,
                      IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(ReserveProductDomainEvent notification, CancellationToken cancellationToken)
    {
        var reserveProductResult = await _mediator.Send(new ReserveProductCommand(
            notification.OrderId,
            notification.ProductId,
            notification.QuantityBought));

        if (!reserveProductResult.IsSuccess)
        {
            await _publishEndpoint.Publish(new ProductReservedFailedIntegrationEvent(notification.OrderId));
            return;
        }

        await _publishEndpoint.Publish(new ProductReservedIntegrationEvent(notification.OrderId));
    }
}
