using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Notifications.Application.Features.SendOrderConfirmation;
using Modules.Notifications.IntegrationEvents;
using Modules.Orders.IntegrationEvents;

namespace Modules.Notifications.Api.MessageConsumers;

public sealed class SendOrderConfirmationConsumer
    : IConsumer<SendOrderConfirmationEmail>
{
    private readonly ILogger<SendOrderConfirmationConsumer> _logger;
    private readonly IMediator _mediator;

    public SendOrderConfirmationConsumer(ILogger<SendOrderConfirmationConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendOrderConfirmationEmail> context)
    {
        await _mediator.Send(new SendOrderConfirmationCommand(context.Message.OrderId));
        await context.Publish(new OrderConfirmationEmailSentIntegrationEvent(context.Message.OrderId));

        // save the message to the database

    }
}
