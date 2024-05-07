using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Persistence.Repositories;

internal class SqlOrderRespository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;
    private readonly IMapper _mapper;

    public SqlOrderRespository(
        OrderDbContext dbContext,
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
        var orderModel = await _dbContext.Orders.Include(o => o.OrderItems).SingleOrDefaultAsync(x => x.Id == id);

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
