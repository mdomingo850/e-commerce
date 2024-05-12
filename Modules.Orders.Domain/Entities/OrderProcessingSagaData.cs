using MassTransit;

namespace Modules.Orders.Domain.Entities;

public class OrderProcessingSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid OrderId { get; set; }
    public bool OrderConfirmationEmailSent { get; set; }
    public bool OrderPaid { get; set; }
    public bool ProductReserved { get; set; }
    public bool OrderProcessingCompleted { get; set; }
}