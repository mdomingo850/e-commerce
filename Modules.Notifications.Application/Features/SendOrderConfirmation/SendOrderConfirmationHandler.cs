using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Notifications.Application.Contracts;
using System;

namespace Modules.Notifications.Application.Features.SendOrderConfirmation;

internal sealed class SendOrderConfirmationHandler : IRequestHandler<SendOrderConfirmationCommand>
{
    private readonly ILogger<SendOrderConfirmationHandler> _logger;
    private readonly INotificationService _notificationService;

    public SendOrderConfirmationHandler(
        ILogger<SendOrderConfirmationHandler> logger, 
        INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Handle(SendOrderConfirmationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Send order confirmation started for orderId {request.OrderId}");
        await _notificationService.SendAsync();
        _logger.LogInformation($"Send order confirmation completed for orderId {request.OrderId}");
    }
}
