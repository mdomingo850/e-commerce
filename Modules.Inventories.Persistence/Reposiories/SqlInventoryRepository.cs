using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Domain.Entities;
using SharedKernel.Persistence;

namespace Modules.Inventories.Persistence.Reposiories;

internal class SqlInventoryRepository : IInventoryRepository
{
    private readonly InventoryDbContext _dbContext;
    private readonly ILogger<SqlInventoryRepository> _logger;

    public SqlInventoryRepository(
        InventoryDbContext dbContext, 
        ILogger<SqlInventoryRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError("Concurrency Exception");
        }
    }

    public Task<Result<bool>> IsProductInStock()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ReduceStockAmount(Guid productId, int quantityToReduce, Guid orderId)
    {
        for (int retryCount = 0; retryCount < 3; retryCount++) // Adjust retry count as needed
        {
            // Refresh product data with current values from database
            var productModel = await _dbContext.Products.FindAsync(productId);
            _dbContext.Entry(productModel).Reload();
            try
            {
                productModel.ReserveProducts(quantityToReduce);

                _dbContext.Update(productModel);
                await _dbContext.SaveChangesAsync();
                //_logger.LogInformation("Stock updated for product for order {orderId}, current stock {stock}", orderId, productModel.Quantity);
                return true; // Stock update successful
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning("Concurrency Exception on attempt {retryCount}, retrying for {orderId}, new stock {stock}...", retryCount + 1, orderId, productModel.Quantity);
                // Implement a wait strategy between retries (optional)
                await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, retryCount))); // Exponential backoff
            }
        }

        _logger.LogError("Failed to update stock for product for order {orderId} after retries", orderId);
        
        return false; // Stock update failed after retries
    }

    public async Task AddOrUpdateInboxMessageAsync(InboxMessage message)
    {
        await _dbContext.AddAsync(message);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasInboxMessage(Guid inboxId)
    {
        return await _dbContext.InboxMessages.AnyAsync(x => x.Id == inboxId);
    }

    public async Task<InboxMessage> GetInboxMessageByIdAsync(Guid id)
    {
        var inboxMessage = await _dbContext.InboxMessages.SingleOrDefaultAsync(x => x.Id == id);

        if (inboxMessage == null)
        {
            return null;
        }

        return inboxMessage;
    }
}
