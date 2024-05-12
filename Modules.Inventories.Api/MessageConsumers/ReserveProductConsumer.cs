using MassTransit;
using MediatR;
using Modules.Inventories.Application.Features.Commands.ReserveProduct;
using Modules.Inventories.IntegrationEvents;
using Modules.Orders.IntegrationEvents;

namespace Modules.Inventories.Api.MessageConsumers;

public sealed class ReserveProductConsumer : IConsumer<ReserveProduct>
{
    private readonly IMediator _mediator;

    public ReserveProductConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ReserveProduct> context)
    {
        await _mediator.Send(new ReserveProductCommand(context.Message.OrderId));

        await context.Publish(new ProductReservedIntegrationEvent(context.Message.OrderId));
    }
}
