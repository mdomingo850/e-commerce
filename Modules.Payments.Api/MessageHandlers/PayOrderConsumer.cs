using MassTransit;
using MediatR;
using Modules.Orders.IntegrationEvents;
using Modules.Payments.Application.Features.Commands.PayOrder;
using Modules.Payments.IntegrationEvents;

namespace Modules.Payments.Api.MessageHandlers;

public sealed class PayOrderConsumer : IConsumer<PayOrder>
{
    private readonly IMediator _mediator;

    public PayOrderConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<PayOrder> context)
    {
        await _mediator.Send(new PayOrderCommand(context.Message.OrderId));
        await context.Publish(new OrderPaidIntegrationEvent(context.Message.OrderId));
    }
}
