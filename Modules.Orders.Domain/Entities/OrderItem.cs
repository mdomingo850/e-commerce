using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Orders.Domain.Entities;

public sealed class OrderItem : Entity
{
    private OrderItem(Guid id) : base(id) { }

    public OrderItem(Guid id, Product product, int quanitity) : base(id)
    {
        Product = product;
        Quanitity = quanitity;
        ProductId = product.Id;
    }

    public static OrderItem Create(Guid id, Product product, int quantity)
    {
        return new OrderItem(id, product, quantity);
    }
    public Guid ProductId { get; init; }
    public Product Product { get; private set; }
    public int Quanitity { get; private set; }
}
