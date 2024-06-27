namespace Modules.Orders.IntegrationEvents;

public sealed record ReserveProduct(Guid OrderId, Guid ProductId, int QuantityBought)
{
}
