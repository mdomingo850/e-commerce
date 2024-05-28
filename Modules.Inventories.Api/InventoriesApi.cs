using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Domain.Entities;

namespace Modules.Inventories.Api;

internal sealed class InventoriesApi : IInventoriesApi
{
    private readonly IInventoryRepository _inventoryRepository;

    public InventoriesApi(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
    {
        var product = await _inventoryRepository.GetProductByIdAsync(id);

        return product is null ? null : new ProductResponse(product.Id, product.Name, product.Price, product.Quantity);
    }

    public async Task UpdateProductAsync(UpdateProductRequest productRequest)
    {
        var product = await _inventoryRepository.GetProductByIdAsync(productRequest.Id);

        if (product is null)
        {
            return;
        }

        product.ReserveProducts(productRequest.QuantityReservedAmount);

        await _inventoryRepository.UpdateProductAsync(product);
    }
}
