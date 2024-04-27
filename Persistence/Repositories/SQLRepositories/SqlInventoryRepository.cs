using Application.Contracts;
using Ardalis.Result;
using Domain.Entities.Products;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories.SQLRepositories;

internal class SqlInventoryRepository : IInventoryRepository
{
    private readonly SharedDbContext _dbContext;

    public SqlInventoryRepository(SharedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
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
