using Domain.Entities.Products;

namespace Domain.Entities.Orders;

public sealed class OrderItem(Product Product, int quanitity)
{
    public Product Product { get; } = Product;
    public int Quanitity { get; } = quanitity;
}
