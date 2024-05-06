using Microsoft.EntityFrameworkCore;
using Modules.Inventories.Domain.Entities;
using Persistence.Models;

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
        modelBuilder.Entity<Product>().ToTable(nameof(Product))
            .OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency).HasMaxLength(3);

            });
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
