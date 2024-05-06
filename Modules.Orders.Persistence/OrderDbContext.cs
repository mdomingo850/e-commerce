using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Persistence.Models;

namespace Modules.Orders.Persistence;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    public OrderDbContext()
    {

    }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToTable(nameof(Order)).Ignore(o => o.Customer);
        modelBuilder.Entity<OrderItem>().ToTable(nameof(OrderItem)).Ignore(oi => oi.Product);
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
