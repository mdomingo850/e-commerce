using Microsoft.EntityFrameworkCore;
using Modules.Customers.Domain.Entities;
using Persistence.Models;

namespace Modules.Customers.Persistence;

public class CustomerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }


    public CustomerDbContext()
    {

    }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable(nameof(Customer));
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
