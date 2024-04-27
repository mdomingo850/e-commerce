using Application.Contracts;
using Application.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Notifications.Commands;

internal class SendOrderConfirmationEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<SendOrderConfirmationEventHandler> _logger;

    public SendOrderConfirmationEventHandler(INotificationService notificationService, ILogger<SendOrderConfirmationEventHandler> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Send order confirmation started");
        await _notificationService.SendAsync();
        _logger.LogInformation("Send order confirmation completed");
    }
}
