using Application.Contracts;
using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Persistence.Models;
using System.Threading;

namespace Persistence.Repositories.SQLRepositories;

internal class SqlOrderRespository : IOrderRepository
{
    private readonly SharedDbContext _dbContext;
    private readonly IMapper _mapper;

    public SqlOrderRespository(
        SharedDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task AddAsync(Order order)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            var outboxMessages = _dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.GetDomainEvents();

                    aggregateRoot.ClearDomainEvents();

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                (
                    Guid.NewGuid(),
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        }),
                    DateTime.UtcNow,
                null,
                    null
                ))
                .ToList();

            _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
            await _dbContext.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        var orderModel = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);

        if (orderModel == null)
        {
            return null;
        }

        return orderModel;
    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}
