using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Contracts;

internal interface IOrderService
{
    Task<Order?> GetByIdAsync(Guid id);
}
