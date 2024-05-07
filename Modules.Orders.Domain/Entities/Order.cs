using Newtonsoft.Json;
using SharedKernel.Domain.Entities.Primitives;
using SharedKernel.Domain.Entities.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public enum OrderStatus
{
    Placed = 1,
    Paid = 2,
    ProductReserved = 3,
    Shipped = 4,
    Cancelled = 5,
    PaymentFailed = 6,
    InventoryReserveFailed = 7,
}

public class Order : AggregateRoot
{
    private readonly HashSet<OrderItem> _orderItems = new();

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Guid CustomerId { get; init; }
    public Customer Customer { get; private set; }
    public DateTime OrderDate { get; init; }
    public OrderStatus OrderStatus { get; private set; }
    //public Money Subtotal { get; }

    private Order(Guid id) : base(id) { }

    private Order(
        Guid id,
        Customer customer,
        HashSet<OrderItem> orderItems,
        DateTime orderDate,
        OrderStatus orderStatus) : base(id)
    {
        CustomerId = customer.Id;
        Customer = customer;
        _orderItems = orderItems;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
    }

    private Order(
        Guid id,
        Guid customerId,
        HashSet<OrderItem> orderItems,
        DateTime orderDate,
        OrderStatus orderStatus) : base(id)
    {
        CustomerId = customerId;
        _orderItems = orderItems;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
    }

    public static Order Create(
        Guid id,
        Customer customer,
        HashSet<OrderItem> orderItems,
        DateTime orderDate,
        OrderStatus orderStatus = OrderStatus.Placed)
    {
        var order = new Order(id, customer, orderItems, orderDate, orderStatus);

        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), id));

        return order;
    }

    public void Add(Product product, int quantity)
    {
        var orderItem = OrderItem.Create(Guid.NewGuid(), product, quantity);

        _orderItems.Add(orderItem);
    }

    public Money CalcualteSubTotal()
    {
        return new Money(
            _orderItems.First().Product.Price.Currency,
            _orderItems.Sum(item => item.Product.Price.Cost));
    }

    public void UpdateOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }

    public void UpdateCustomer(Customer customer)
    {

    }
}
