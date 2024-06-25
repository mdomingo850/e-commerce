using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Payments.Application.Contracts;

namespace Modules.Payments.Application.Features.Commands.PayOrder;

internal class ReverseOrderPaymentCommandHandler : IRequestHandler<PayOrderCommand>
{
    private readonly ILogger<ReverseOrderPaymentCommandHandler> _logger;
    private readonly IPaymentService _paymentService;

    public ReverseOrderPaymentCommandHandler(ILogger<ReverseOrderPaymentCommandHandler> logger, IPaymentService paymentService)
    {
        _logger = logger;
        _paymentService = paymentService;
    }

    public async Task Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Pay order started for orderId: {request.OrderId}");
        await _paymentService.PayAsync();
        _logger.LogInformation($"Pay order completed for orderId: {request.OrderId}");
    }
}
