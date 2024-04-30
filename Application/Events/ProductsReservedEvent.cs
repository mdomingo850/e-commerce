using MediatR;

namespace Application.Events;

internal record ProductsReservedEvent(Guid OrderId) : INotification
{
}
