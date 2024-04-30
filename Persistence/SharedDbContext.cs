
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Models;


namespace Persistence;

internal class SharedDbContext : DbContext
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

    //public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    using var transaction = Database.BeginTransaction();

    //    try
    //    {
    //        var result = await base.SaveChangesAsync(cancellationToken);

    //        var outboxMessages = ChangeTracker
    //        .Entries<AggregateRoot>()
    //        .Select(x => x.Entity)
    //        .SelectMany(aggregateRoot =>
    //        {
    //            var domainEvents = aggregateRoot.GetDomainEvents();

    //            aggregateRoot.ClearDomainEvents();

    //            return domainEvents;
    //        })
    //        .Select(domainEvent => new OutboxMessage
    //        (
    //            Guid.NewGuid(),
    //            domainEvent.GetType().Name,
    //            JsonConvert.SerializeObject(
    //                domainEvent,
    //                new JsonSerializerSettings
    //                {
    //                    TypeNameHandling = TypeNameHandling.All
    //                }),
    //            DateTime.UtcNow,
    //            null,
    //            null
    //        ))
    //        .ToList();

    //        Set<OutboxMessage>().AddRange(outboxMessages);
    //        await base.SaveChangesAsync(cancellationToken);

    //        transaction.Commit();

    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        transaction.Rollback();
    //        throw;
    //    }

    //}
}
