using MediatR;

namespace Modules.Notifications.Application.Features.SendOrderConfirmation;

public record SendOrderConfirmationCommand(Guid OrderId) : IRequest
{
}
