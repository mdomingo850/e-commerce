
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Models;


namespace Persistence;

public class SharedDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OutboxMessage> OutboxMessages{ get; set; }


    public SharedDbContext()
    {
        
    }

    public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
        modelBuilder.Entity<Order>().ToTable(nameof(Order));
        modelBuilder.Entity<OrderItem>().ToTable(nameof(OrderItem));
        modelBuilder.Entity<Product>().ToTable(nameof(Product))
            .OwnsOne(p => p.Price, priceBuilder =>
            {
                priceBuilder.Property(m => m.Currency).HasMaxLength(3);

            });
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
