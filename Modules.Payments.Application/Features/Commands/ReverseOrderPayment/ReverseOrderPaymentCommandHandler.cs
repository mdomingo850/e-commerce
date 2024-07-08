using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Payments.Application.Contracts;
using Modules.Payments.Application.Features.Commands.PayOrder;

namespace Modules.Payments.Application.Features.Commands.ReverseOrderPayment;

internal class ReverseOrderPaymentCommandHandler : IRequestHandler<ReverseOrderPaymentCommand>
{
    private readonly ILogger<ReverseOrderPaymentCommandHandler> _logger;
    private readonly IPaymentService _paymentService;

    public ReverseOrderPaymentCommandHandler(ILogger<ReverseOrderPaymentCommandHandler> logger, IPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;
    }

    public async Task Handle(ReverseOrderPaymentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Reverse order payment started for orderId: {request.OrderId}");
        await _paymentService.UndoPaymentAsync();
        _logger.LogInformation($"Reverse order payment completed for orderId: {request.OrderId}");
    }
}
