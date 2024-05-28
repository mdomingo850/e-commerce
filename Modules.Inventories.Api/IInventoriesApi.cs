using Modules.Inventories.Domain.Entities;

namespace Modules.Inventories.Api;

public interface IInventoriesApi
{
    Task UpdateProductAsync(UpdateProductRequest product);
    Task<ProductResponse?> GetProductByIdAsync(Guid id);
}
