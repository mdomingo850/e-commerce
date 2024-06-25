using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Application.Contracts;

namespace Modules.Inventories.Application.Features.Commands.ReserveProduct;

internal class ReserveProductCommandHandler : IRequestHandler<ReserveProductCommand, Result>
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

    public async Task<Result> Handle(ReserveProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Reserve product started");

        var product = await _inventoryRepository.GetProductByIdAsync(request.ProductId);

        if (product is null)
        {
            _logger.LogError("Product not found");
            return Result.Error("Product not found");
        }

        return Result.Error("Failed to reduce stock amount");

        var reduceStockAmountResult = await _inventoryRepository.ReduceStockAmount(product.Id, request.QuantityBought, request.OrderId);

        if (!reduceStockAmountResult)
            return Result.Error("Failed to reduce stock amount");

        _logger.LogInformation("Reserve product completed");

        return Result.Success();
    }
}
