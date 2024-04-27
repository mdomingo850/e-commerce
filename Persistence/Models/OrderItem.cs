using Domain.Entities.Products;

namespace Persistence.Models;

internal class OrderItem
{
    public int Id { get; set; }
    public required Product Product { get; set; }
    public int Quanitity { get; set; }
}
