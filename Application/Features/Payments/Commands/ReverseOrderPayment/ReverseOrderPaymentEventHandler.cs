//using Application.Contracts;
//using Application.Events;
//using MediatR;

//namespace Application.Features.Payments.Commands.ReverseOrderPayment;

//internal class ReverseOrderPaymentEventHandler
//{
//    private readonly IPaymentService _paymentService;
//    private readonly IPublisher _mediator;

//    public ReverseOrderPaymentEventHandler(
//        IPaymentService paymentService,
//        IPublisher mediator)
//    {
//        _paymentService = paymentService;
//        _mediator = mediator;
//    }

//    public async Task Handle(ReserveProductsFailedEvent message)
//    {
//        await _paymentService.UndoPaymentAsync();

//        await _mediator.Publish(new PaymentReversedEvent(message.OrderId));
//    }
//}
