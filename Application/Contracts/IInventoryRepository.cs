using Ardalis.Result;
using Domain.Entities.Products;

namespace Application.Contracts;

public interface IInventoryRepository
{
    Task UpdateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(int id);
}
