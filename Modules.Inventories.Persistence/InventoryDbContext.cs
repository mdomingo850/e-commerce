using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Domain.Entities;
using SharedKernel.Domain.Entities.ValueObjects;
using SharedKernel.Persistence;

namespace Modules.Inventories.Persistence;

public class InventoryDbContext : DbContext
{
    private readonly ILogger<InventoryDbContext> _logger;

    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    public InventoryDbContext(ILogger<InventoryDbContext> logger)
    {
        _logger = logger;
        _logger.LogInformation("InventoryDbContext created");
    }

    public InventoryDbContext(DbContextOptions<
        InventoryDbContext> options, 
        ILogger<InventoryDbContext> logger) : base(options)
    {
        _logger = logger;
        //_logger.LogInformation("InventoryDbContext created");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable(nameof(Product));
            p.HasData(Product.Create(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), "Google Nest", 1000));
            p.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency).HasMaxLength(3);
                priceBuilder.HasData(new { ProductId = Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), Currency = "$", Cost = 39.99m });
            });
        });
            
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
