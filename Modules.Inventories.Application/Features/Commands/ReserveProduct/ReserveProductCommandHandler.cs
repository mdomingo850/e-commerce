using MediatR;
using Microsoft.Extensions.Logging;

namespace Modules.Inventories.Application.Features.Commands.ReserveProduct;

internal class ReserveProductCommandHandler : IRequestHandler<ReserveProductCommand>
{
    private readonly ILogger<ReserveProductCommandHandler> _logger;

    public ReserveProductCommandHandler(ILogger<ReserveProductCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ReserveProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Reserve product started");
        await Task.Delay(1000);
        _logger.LogInformation("Reserve product completed");
    }
}
