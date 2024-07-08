namespace Modules.Orders.IntegrationEvents;

public sealed record OrderCreatedIntegrationEvent(Guid OrderId, Guid ProductId, int QuantityBought)
{

}
