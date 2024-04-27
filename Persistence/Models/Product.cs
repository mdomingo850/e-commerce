namespace Persistence.Models;

internal class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Currency { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
