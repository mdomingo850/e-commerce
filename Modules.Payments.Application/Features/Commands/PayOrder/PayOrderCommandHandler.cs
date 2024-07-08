using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Payments.Application.Contracts;
using SharedKernel.Models;

namespace Modules.Payments.Application.Features.Commands.PayOrder;

internal class ReverseOrderPaymentCommandHandler : IRequestHandler<PayOrderCommand>
{
    private readonly ILogger<ReverseOrderPaymentCommandHandler> _logger;
    private readonly IPaymentService _paymentService;
    private readonly IEnvironmentVariables _environmentVariables;

    public ReverseOrderPaymentCommandHandler(
        ILogger<ReverseOrderPaymentCommandHandler> logger, 
        IPaymentService paymentService, 
        IEnvironmentVariables environmentVariables)
    {
        _logger = logger;
        _paymentService = paymentService;
        _environmentVariables = environmentVariables;
    }

    public async Task Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{_environmentVariables.ApiName} - Pay order started for orderId: {request.OrderId}");
        await _paymentService.PayAsync();
        _logger.LogInformation($"{_environmentVariables.ApiName} - Pay order completed for orderId: {request.OrderId}");
    }
}
