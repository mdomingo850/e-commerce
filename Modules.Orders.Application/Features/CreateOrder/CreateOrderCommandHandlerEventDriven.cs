using Ardalis.Result;
using MediatR;
using Modules.Customers.Api;
using Modules.Inventories.Api;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Domain.Entities;
using Modules.Payments.Api;

namespace Modules.Orders.Application.Features.CreateOrder;

/// <summary>
/// Pros:
///     Single responsibility - Only responsible for creating order
///     Extensible - If I want to add a Recommendation service, I do not have to modify this create order service
///     Performance - Faster response time. Better UX 
///                     Resources are freed. 
///                     No Timeout issues
///     Loose coupling - Each service is unaware of each other. Easier to maintain as application/team scales. 
///         Loose Temporal coupling due to async communication
///     High Availability - If the payment service is down, then you can still create orders. If order service is down, then you can still process existing orders.
/// Cons:
///     Asynchronous - Not as easy to follow
///     Latency - Network or messaging latency
///     More Complex - Need to use a messaging system
///     Consistency - Eventually consistent
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly ICustomersApi _customersApi;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentsApi _paymentsApi;
    private readonly IInventoriesApi _inventoriesApi;

    public CreateOrderCommandHandler(
        ICustomersApi customersApi,
        IOrderRepository orderRepository,
        IPaymentsApi paymentsApi,
        IInventoriesApi inventoriesApi)
    {
        _customersApi = customersApi;
        _orderRepository = orderRepository;
        _paymentsApi = paymentsApi;
        _inventoriesApi = inventoriesApi;
    }

    public async Task<Result> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken = default)
    {
        #region Validation
        //get customer by id
        var customerResponse = await _customersApi.GetByIdAsync(request.CustomerId);

        if (customerResponse is null)
            return Result.NotFound();

        //payment validation
        var validatePaymentOptionResult = await _paymentsApi.ValidatePaymentOptionAsync();

        if (!validatePaymentOptionResult.IsSuccess)
            return Result.Error([.. validatePaymentOptionResult.Errors]);

        var isPaymentOptionValid = validatePaymentOptionResult.Value;

        if (!isPaymentOptionValid)
            return Result.Conflict();

        //inventory in-stock validation
        var firstOrderItem = request.OrderItems.First();
        var firstProductId = firstOrderItem.Item1;
        var productResponse = await _inventoriesApi.GetProductByIdAsync(firstProductId);

        if (productResponse is null)
            return Result.NotFound();

        var product = Product.Create(productResponse.Id, productResponse.Name, productResponse.Price, productResponse.Quantity);

        var firstProductQuanitity = firstOrderItem.Item2;
        if (!product.IsInStock(firstProductQuanitity))
            return Result.Conflict();
        #endregion
        var orderItem = OrderItem.Create(Guid.NewGuid(), product, firstProductQuanitity);

        var customer = Customer.Create(customerResponse.Id, customerResponse.Name);

        var order = Order.Create(Guid.NewGuid(), customer, new HashSet<OrderItem>() { orderItem }, DateTime.UtcNow);

        await _orderRepository.AddAsync(order);

        //await _publisher.Publish(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id, orderItem));
        //await _messageBus.Publish(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id, orderItem));
        return Result.Success();
    }
}
