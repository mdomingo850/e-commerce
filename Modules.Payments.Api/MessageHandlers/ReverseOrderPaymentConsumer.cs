using MassTransit;
using MediatR;
using Modules.Orders.IntegrationEvents;
using Modules.Payments.Application.Features.Commands.ReverseOrderPayment;
using Modules.Payments.IntegrationEvents;

namespace Modules.Payments.Api.MessageHandlers;

public sealed class ReverseOrderPaymentConsumer : IConsumer<ReverseOrderPayment>
{
    private readonly IMediator _mediator;

    public ReverseOrderPaymentConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ReverseOrderPayment> context)
    {
        await _mediator.Send(new ReverseOrderPaymentCommand(context.Message.OrderId));
        await context.Publish(new OrderPaymentReversedIntegrationEvent(context.Message.OrderId));
    }
}
