namespace Modules.Orders.IntegrationEvents;

public sealed record ReverseOrderPayment(Guid OrderId)
{
}
