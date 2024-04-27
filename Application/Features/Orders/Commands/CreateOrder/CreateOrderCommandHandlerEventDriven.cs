using Application.Contracts;
using Application.Events;
using Ardalis.Result;
using Domain.Entities.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

/// <summary>
/// Cons:
///     Performance - One request is long running - bad UX. 
///                     It also holding on to the resources for a long time. 
///                     Timeout issues
///     Single responsibility - This class is responsible for creating the order and processing the order
///                             Hard to maintain the processing logic and the compensation rollback logic
///     Tight coupling - Each service is coupled to each other. Harder to maintain as application/team scales.
///     Low Availability - If the payment service is down, then no one can create orders.
///     Not as Extensible - If I want to add a Recommendation service, I would have to modify this create order service
/// Pros:
///     Atomic - Saved as a single transaction
///     Synchronous - Easy to follow
///     No Latency - No network or messaging latency
///     Less Complex - no need to use a messaging system
///     Consistency - Strongly consistent
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentService _paymentService;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly INotificationService _notificationService;
    private readonly IPublisher _mediator;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository, 
        IOrderRepository orderRepository,
        IPaymentService paymentService,
        IInventoryRepository inventoryRepository,
        INotificationService notification,
        IPublisher mediator)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _paymentService = paymentService;
        _inventoryRepository = inventoryRepository;
        _notificationService = notification;
        _mediator = mediator;
    }

    public async Task<Result> Handle(
        CreateOrderCommand request, 
        CancellationToken cancellationToken = default)
    {
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

        var orderItem = new OrderItem(product, firstProductQuanitity);

        var order = Order.Create(customer, new HashSet<OrderItem>() { orderItem }, DateTime.UtcNow);

        await _orderRepository.AddAsync(order);

        await _mediator.Publish(new OrderCreatedEvent(order.Id, orderItem));

        return Result.Success();
    }
}
