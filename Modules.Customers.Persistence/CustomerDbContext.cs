﻿using Microsoft.EntityFrameworkCore;
using Modules.Customers.Domain.Entities;
using SharedKernel.Persistence;

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
        modelBuilder
            .Entity<Customer>()
            .ToTable(nameof(Customer))
            .HasData(Customer.Create(Guid.Parse("AC8572BA-8742-43BE-AC63-FD69654A7188"), "Clark", "Kent"));
        modelBuilder.Entity<OutboxMessage>().ToTable(nameof(OutboxMessages));
    }
}
