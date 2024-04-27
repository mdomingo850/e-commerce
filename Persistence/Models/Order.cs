using Domain.Entities.Customers;
using Domain.Entities.Orders;

namespace Persistence.Models;

internal sealed class Order
{
    public int Id { get; set; }
    public required ICollection<OrderItem> OrderItems { get; set; }

    public int CustomerId { get; set; }

    public required Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
