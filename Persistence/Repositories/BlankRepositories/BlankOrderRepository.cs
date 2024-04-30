using Application.Contracts;
using Ardalis.Result;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Persistence.Repositories.BlankRepositories;

public class BlankOrderRepository : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await Task.Delay(2000);
    }

    public async Task<Order> GetByIdAsync(Guid id)
    {
        var customer = Customer.Create(Guid.NewGuid(), "Clark", "Kent");
        var product = Product.Create(Guid.NewGuid(), "Google Nest", new Money("$", 1), 5);
        var orderItem = OrderItem.Create(product.Id, product, 100);
        var orderItems = new HashSet<OrderItem>() { orderItem };

        return Order.Create(id, customer, orderItems, DateTime.UtcNow, OrderStatus.Placed);
    }

    public async Task UpdateAsync(Order order)
    {
        await Task.Delay(2000);
    }
}
