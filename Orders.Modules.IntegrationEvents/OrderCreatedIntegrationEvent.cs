namespace Orders.Modules.IntegrationEvents
{
    public record OrderCreatedIntegrationEvent(Guid OrderId)
    { }
}
