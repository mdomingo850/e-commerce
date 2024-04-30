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
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
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
