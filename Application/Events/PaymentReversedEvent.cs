namespace Application.Events;

internal record PaymentReversedEvent(Guid OrderId)
{
}
