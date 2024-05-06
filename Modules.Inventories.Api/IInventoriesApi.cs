using Modules.Inventories.Domain.Entities;

namespace Modules.Inventories.Api;

public interface IInventoriesApi
{
    Task UpdateProductAsync(Product product);
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
}
