using Domain.Entities.Products;
using Domain.Primitives;

namespace Domain.Entities.Orders;

public sealed class OrderItem : Entity
{
    private OrderItem(Guid id) : base(id) { }

    public OrderItem(Guid id, Product product, int quanitity) : base(id)
    {
        Product = product;
        Quanitity = quanitity;
    }

    public static OrderItem Create(Guid id, Product product, int quantity)
    {
        return new OrderItem(id, product, quantity);
    }
    public Product Product { get; private set; }
    public int Quanitity { get; private set; } 
}
