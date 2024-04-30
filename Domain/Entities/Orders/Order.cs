using Domain.Entities.Customers;
using Domain.Entities.Products;
using Domain.Primitives;
using Domain.ValueObjects;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Domain.Entities.Orders;

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

    public Customer Customer { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus OrderStatus { get; private set; }
    //public Money Subtotal { get; }

    private Order(int id = 0) : base(id) { }

    private Order(
        int id,
        Customer customer,
        HashSet<OrderItem> orderItems,
        DateTime orderDate,
        OrderStatus orderStatus) : base(id)
    {
        Customer = customer;
        _orderItems = orderItems;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
    }

    public static Order Create(
        Customer customer,
        HashSet<OrderItem> orderItems,
        DateTime orderDate,
        OrderStatus orderStatus = OrderStatus.Placed,
        int id = 0)
    {
        var order = new Order(id, customer, orderItems, orderDate, orderStatus);

        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), order, order.OrderItems.First()));

        return order;
    }

    public void Add(Product product, int quantity)
    {
        var orderItem = OrderItem.Create(product, quantity);
        
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
}
