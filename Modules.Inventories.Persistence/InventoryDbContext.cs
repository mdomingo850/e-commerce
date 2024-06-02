using Microsoft.EntityFrameworkCore;
using Modules.Inventories.Domain.Entities;
using SharedKernel.Domain.Entities.ValueObjects;
using SharedKernel.Persistence;

namespace Modules.Inventories.Persistence;

public class InventoryDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    public InventoryDbContext()
    {

    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable(nameof(Product));
            p.HasData(Product.Create(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), "Google Nest", 100));
            p.OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency).HasMaxLength(3);
                priceBuilder.HasData(new { ProductId = Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), Currency = "$", Cost = 39.99m });
            });
        });
            
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
