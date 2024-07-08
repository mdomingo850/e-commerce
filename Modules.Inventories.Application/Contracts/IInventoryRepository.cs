using Modules.Inventories.Domain.Entities;

namespace Modules.Inventories.Application.Contracts;

public interface IInventoryRepository
{
    Task UpdateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<bool> ReduceStockAmount(Guid productId, int quantityToReduce, Guid orderId);
}
