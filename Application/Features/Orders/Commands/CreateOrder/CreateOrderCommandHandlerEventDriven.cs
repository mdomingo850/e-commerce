using Application.Contracts;
using Application.Events;
using Ardalis.Result;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

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
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPublisher _publisher;
    private readonly IMessageBus _messageBus;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository, 
        IOrderRepository orderRepository,
        IPaymentService paymentService,
        IInventoryRepository inventoryRepository,
        IPublisher publisher,
        IMessageBus messageBus)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _inventoryRepository = inventoryRepository;
        _publisher = publisher;
        _messageBus = messageBus;
    }

    public async Task<Result> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken = default)
    {
		#region Validation
		//get customer by id
		var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
            return Result.NotFound();

        //payment validation
        var validatePaymentOptionResult = await _paymentService.ValidatePaymentOptionAsync();

        if (!validatePaymentOptionResult.IsSuccess)
            return Result.Error([.. validatePaymentOptionResult.Errors]);

        var isPaymentOptionValid = validatePaymentOptionResult.Value;

        if (!isPaymentOptionValid)
            return Result.Conflict();

        //inventory in-stock validation
        var firstOrderItem = request.OrderItems.First();
        var firstProductId = firstOrderItem.Item1;
        var product = await _inventoryRepository.GetProductByIdAsync(firstProductId);

        if (product is null)
            return Result.NotFound();

        var firstProductQuanitity = firstOrderItem.Item2;
        if (!product.IsInStock(firstProductQuanitity))
            return Result.Conflict();
		#endregion
		var orderItem = OrderItem.Create(Guid.NewGuid(), product, firstProductQuanitity);

        var order = Order.Create(Guid.NewGuid(), customer, new HashSet<OrderItem>() { orderItem }, DateTime.UtcNow);

        await _orderRepository.AddAsync(order);

        await _publisher.Publish(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id, orderItem));
        //await _messageBus.Publish(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id, orderItem));
        return Result.Success();
    }
}
