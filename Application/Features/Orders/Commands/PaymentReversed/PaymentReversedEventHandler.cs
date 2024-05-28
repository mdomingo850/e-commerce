//using Application.Contracts;
//using Application.Events;
//using Domain.Entities.Orders;

//namespace Application.Features.Orders.Commands.PaymentReversed;

//internal class PaymentReversedEventHandler
//{
//    private readonly IOrderRepository _orderRepository;

//    public PaymentReversedEventHandler(IOrderRepository orderRepository)
//    {
//        _orderRepository = orderRepository;
//    }

//    public async Task Handle(PaymentReversedEvent message)
//    {
//        var order = await _orderRepository.GetByIdAsync(message.OrderId);

//        order.UpdateOrderStatus(OrderStatus.Cancelled);

//        await _orderRepository.UpdateAsync(order);
//    }
//}
