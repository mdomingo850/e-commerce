//using Application.Contracts;
//using Application.Events;
//using Ardalis.Result;
//using Domain.Entities.Orders;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace Application.Features.Payments.Commands.PayOrder;

//internal class PayOrderEventHandler : INotificationHandler<OrderCreatedDomainEvent>
//{
//    private readonly IPaymentService _paymentService;
//    private readonly IPublisher _mediator;
//    private readonly ILogger<PayOrderEventHandler> _logger;

//    public PayOrderEventHandler(
//        IPaymentService paymentService,
//        IPublisher mediator,
//        ILogger<PayOrderEventHandler> logger)
//    {
//        _paymentService = paymentService;
//        _mediator = mediator;
//        _logger = logger;
//    }

//    public async Task Handle(OrderCreatedDomainEvent message, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Pay order started");

//        //payment processing
//        var payResult = await _paymentService.PayAsync();
        
//        _logger.LogInformation("Pay order completed");


//        if (!payResult.IsSuccess)
//        {
//            await _mediator.Publish(new OrderPaymentFailedEvent(message.OrderId));
//        }

//        await _mediator.Publish(new OrderPaidEvent(message.OrderId, message.OrderItem));


//        return;
//    }
//}
