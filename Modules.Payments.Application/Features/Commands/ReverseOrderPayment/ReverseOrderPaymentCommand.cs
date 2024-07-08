using MediatR;

namespace Modules.Payments.Application.Features.Commands.ReverseOrderPayment;

public record class ReverseOrderPaymentCommand(Guid OrderId) : IRequest
{
}
