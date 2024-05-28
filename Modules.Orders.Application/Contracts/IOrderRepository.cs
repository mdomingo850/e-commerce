using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Contracts;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task UpdateAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id);
}
