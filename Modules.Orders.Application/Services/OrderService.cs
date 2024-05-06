using Modules.Customers.Api;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository, ICustomersApi customersApi) : IOrderService
{
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var order = await orderRepository.GetByIdAsync(id);

        if (order is null)
        {
            return null;
        }

        var customerResponse = await customersApi.GetByIdAsync(order.CustomerId);

        if (customerResponse is null)
        {
            return null;
        }

        var customer = Customer.Create(customerResponse.Id, customerResponse.Name);

        order.UpdateCustomer(customer);

        return order;
    }
}
