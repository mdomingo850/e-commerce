using MassTransit;
using Microsoft.Extensions.Logging;
using Modules.Orders.IntegrationEvents;

namespace Modules.Orders.Api.MessageConsumers;

public sealed class OrderProcessingCompletedConsumer : IConsumer<CompleteOrderProcessing>
{
    private readonly ILogger<OrderProcessingCompletedConsumer> _logger;

    public OrderProcessingCompletedConsumer(ILogger<OrderProcessingCompletedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CompleteOrderProcessing> context)
    {
        _logger.LogInformation($"Order processing completed");

        return Task.CompletedTask;
    }
}
