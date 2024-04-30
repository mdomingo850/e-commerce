using Domain.Entities.Orders;

namespace Application.Contracts;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task UpdateAsync(Order order);

    Task<Order?> GetByIdAsync(Guid id);
}
