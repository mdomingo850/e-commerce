using MassTransit;
using Modules.Inventories.IntegrationEvents;
using Modules.Notifications.IntegrationEvents;
using Modules.Orders.Domain.Entities;
using Modules.Orders.IntegrationEvents;
using Modules.Payments.IntegrationEvents;

namespace Modules.Orders.Api.Sagas;

public class OrderProcessingSaga : MassTransitStateMachine<OrderProcessingSagaData>
{
    public State OrderConfirmationEmailSending { get; set; }
    public State PayingOrder { get; set; }
    public State ProductReserving { get; set; }
    public State OrderProcessFinalizing { get; set; }
    public State ReversingOrderPayment { get; set; }

    public Event<OrderCreatedIntegrationEvent> OrderCreated { get; set; }
    public Event<OrderConfirmationEmailSentIntegrationEvent> OrderConfirmationEmailSent { get; set; }
    public Event<OrderPaidIntegrationEvent> OrderPaid { get; set; }
    public Event<ProductReservedIntegrationEvent> ProductReserved { get; set; }
    public Event<ProductReservedFailedIntegrationEvent> ProductReservedFailed { get; set; }
    public Event<OrderPaymentReversedIntegrationEvent> OrderPaymentReversed { get; set; }

    public OrderProcessingSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderCreated, e => e.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderConfirmationEmailSent, e => e.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderPaid, e => e.CorrelateById(m => m.Message.OrderId));
        Event(() => ProductReserved, e => e.CorrelateById(m => m.Message.OrderId));
        Event(() => ProductReservedFailed, e => e.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderPaymentReversed, e => e.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(OrderCreated)
                .Then(context =>
                {
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.ProductId = context.Message.ProductId;
                    context.Saga.QuantityBought = context.Message.QuantityBought;
                    context.Saga.StartDate = DateTime.UtcNow;
                })
                .TransitionTo(OrderConfirmationEmailSending)
                .Publish(context => new SendOrderConfirmationEmail(context.Message.OrderId)));

        During(OrderConfirmationEmailSending,
            When(OrderConfirmationEmailSent)
                .Then(context => context.Saga.OrderConfirmationEmailSent = true)
                .TransitionTo(PayingOrder)
                .Publish(context => new PayOrder(context.Message.OrderId)));

        During(PayingOrder,
            When(OrderPaid)
                .Then(context => context.Saga.OrderPaid = true)
                .TransitionTo(ProductReserving)
                .Publish(context => new ReserveProduct(
                    context.Message.OrderId,
                    context.Saga.ProductId,
                    context.Saga.QuantityBought)));

        During(ProductReserving,
            When(ProductReserved)
                .Then(context =>
                {
                    context.Saga.ProductReserved = true;
                    context.Saga.OrderProcessingCompleted = true;
                    context.Saga.EndDate = DateTime.UtcNow; 
                    context.Saga.OrderProcessingSucceeded = true;
                })
                .TransitionTo(OrderProcessFinalizing)
                .Publish(context => new CompleteOrderProcessing(context.Message.OrderId))
                .Finalize());

        During(ProductReserving,
            When(ProductReservedFailed)
                .TransitionTo(ReversingOrderPayment)
                .Publish(context => new ReverseOrderPayment(context.Message.OrderId)));

        During(ReversingOrderPayment,
            When(OrderPaymentReversed)
                .Then(context => 
                {
                    context.Saga.OrderPaymentReversed = true;
                    context.Saga.OrderProcessingCompleted = true;
                    context.Saga.EndDate = DateTime.UtcNow;
                    context.Saga.OrderProcessingSucceeded = false;
                })
                .TransitionTo(OrderProcessFinalizing)
                .Publish(context => new CompleteOrderProcessing(context.Message.OrderId))
                .Finalize());
    }
}
