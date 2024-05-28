using MassTransit;
using MediatR;
using Modules.Orders.Domain.Entities;
using Modules.Orders.IntegrationEvents;

namespace Modules.Orders.Application.Features.OrderCreated;

internal class OrderCreatedEventHandlerEventDriven : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderCreatedEventHandlerEventDriven(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(new OrderCreatedIntegrationEvent(notification.OrderId));
    }
}
