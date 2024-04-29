
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;


namespace Persistence;

internal class SharedDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    //public DbSet<Payment> Payments { get; set; }
    //public DbSet<Shipment> Shipments { get; set; }

    public SharedDbContext()
    {
        
    }

    public SharedDbContext(DbContextOptions<SharedDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\ECommerce;Database=SingleShared;Integrated Security=true;");
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
        //modelBuilder.Entity<Payment>().ToTable(nameof(Payment));
        //modelBuilder.Entity<Shipment>().ToTable(nameof(Shipment));
    }
}
