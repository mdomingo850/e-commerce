using Ardalis.Result;
using Newtonsoft.Json;
using SharedKernel.Domain.Entities.Primitives;
using SharedKernel.Domain.Entities.ValueObjects;

namespace Modules.Inventories.Domain.Entities;

public sealed class Product : AggregateRoot
{
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }

    private Product(Guid id) : base(id)
    {

    }



    private Product(Guid id, string name, string currency, decimal cost, int quantity) : base(id)
    {
        Id = id;
        Name = name;
        Price = new Money(currency, cost);
        Quantity = quantity;
    }

    [JsonConstructor]
    private Product(Guid id, string name, Money price, int quantity) : base(id)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    private Product(Guid id, string name, int quantity) : base(id)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }

    public static Product Create(Guid id, string name, Money price, int quantity)
    {
        return new Product(id, name, price, quantity);
    }

    public static Product Create(Guid id, string name, int quantity)
    {
        return new Product(id, name, quantity);
    }

    public static Product Create(Guid id, string name, string currency, decimal cost, int quantity)
    {
        return new Product(id, name, currency, cost, quantity);
    }

    public bool IsInStock(int quantityRequested)
    {
        if (quantityRequested > Quantity) return false; return true;
    }

    public Result ReserveProducts(int quantityRequested)
    {
        if (!IsInStock(quantityRequested)) return Result.Conflict();

        Quantity -= quantityRequested;

        return Result.Success();
    }
}
