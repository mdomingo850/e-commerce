using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Domain.Entities;

namespace Modules.Inventories.Persistence.Reposiories;

internal class SqlInventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _dbContext;

    public SqlInventoryRepository(InventoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var productModel = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);

        if (productModel == null)
        {
            return null;
        }

        return productModel;
    }

    public async Task UpdateProductAsync(Product product)
    {
        _dbContext.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Result<bool>> IsProductInStock()
    {
        throw new NotImplementedException();
    }
}
