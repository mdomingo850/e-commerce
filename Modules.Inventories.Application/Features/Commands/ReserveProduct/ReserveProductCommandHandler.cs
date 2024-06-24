using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Application.Contracts;

namespace Modules.Inventories.Application.Features.Commands.ReserveProduct;

internal class ReserveProductCommandHandler : IRequestHandler<ReserveProductCommand>
{
    private readonly ILogger<ReserveProductCommandHandler> _logger;
    private readonly IInventoryRepository _inventoryRepository;

    public ReserveProductCommandHandler(
        ILogger<ReserveProductCommandHandler> logger, 
        IInventoryRepository inventoryRepository)
    {
        _logger = logger;
        _inventoryRepository = inventoryRepository;
    }

    public async Task Handle(ReserveProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Reserve product started");
        var product = await _inventoryRepository.GetProductByIdAsync(request.ProductId);

        if (product is null)
        {
            _logger.LogError("Product not found");
            return;
        }

        await _inventoryRepository.ReduceStockAmount(product.Id, request.QuantityBought, request.OrderId);

        _logger.LogInformation("Reserve product completed");
    }
}
