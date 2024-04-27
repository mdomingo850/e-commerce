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

    public async Task<Order> GetByIdAsync(int id)
    {
        var customer = Customer.Create(1, "Clark", "Kent");
        var product = Product.Create(1, "Google Nest", new Money("$", 1), 5);
        var orderItem = new OrderItem(product, 100);
        var orderItems = new HashSet<OrderItem>() { orderItem };

        return Order.Create(customer, orderItems, DateTime.UtcNow, OrderStatus.Placed, id);
    }

    public async Task UpdateAsync(Order order)
    {
        await Task.Delay(2000);
    }
}
