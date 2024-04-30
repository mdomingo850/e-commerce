using Application.Contracts;
using Ardalis.Result;
using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Persistence.Repositories.BlankRepositories;

public class BlankInventoryRepository : IInventoryRepository
{
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        await Task.Delay(2000);

        return Product.Create(Guid.NewGuid(), "Google Nest", new Money("$", 1), 5);
    }

    public async Task<Result<bool>> IsProductInStock()
    {
        await Task.Delay(2000);

        return Result.Success(true);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await Task.Delay(2000);
    }
}
