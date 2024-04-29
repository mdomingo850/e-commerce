using Domain.Entities.Products;

namespace Domain.Entities.Orders;

public sealed class OrderItem
{

    private OrderItem() { }

    private OrderItem(Product product, int quanitity)
    {
        Product = product;
        Quanitity = quanitity;
    }

    public static OrderItem Create(Product product, int quantity)
    {
        return new OrderItem(product, quantity);
    }

    public int Id { get; private set; }
    public Product Product { get; private set; }
    public int Quanitity { get; private set; } 
}
