using MediatR;

namespace Application.Events;

internal record ProductsReservedEvent(int OrderId) : INotification
{
}
