using Domain.ValueObjects;
using Ardalis.Result;

namespace Domain.Entities.Products;

public sealed class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }

    private Product()
    {

    }

    private Product(int id, string name, string currency, decimal cost, int quantity)
    {
        Id = id;
        Name = name;
        Price = new Money(currency, cost);
        Quantity = quantity;
    }

    private Product(int id, string name, Money price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public static Product Create(int id, string name, Money price, int quantity)
    {
        return new Product(id, name, price, quantity);
    }

    public static Product Create(int id, string name, string currency, decimal cost, int quantity)
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
