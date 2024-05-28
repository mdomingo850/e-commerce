using MediatR;

namespace Modules.Payments.Application.Features.Commands.PayOrder;

public sealed record PayOrderCommand(Guid OrderId) : IRequest
{
}
