using Modules.Customers.Api;
using Modules.Inventories.Api;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository, ICustomersApi customersApi, IInventoriesApi inventoryApi) : IOrderService
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

        foreach (var orderItem in order.OrderItems)
        {
            var productResponse = await inventoryApi.GetProductByIdAsync(orderItem.ProductId);
            var product = Product.Create(productResponse.Id, productResponse.Name, productResponse.Price, productResponse.Quantity);
            orderItem.UpdateProduct(product);
        }

        return order;
    }

    public async Task UpdateAsync(Order order)
    {
        await orderRepository.UpdateAsync(order);
    }
}
