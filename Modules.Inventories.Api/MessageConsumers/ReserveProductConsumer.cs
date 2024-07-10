using MassTransit;
using MassTransit.Transports;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Application.Features.Commands.ReserveProduct;
using Modules.Inventories.Domain.Entities;
using Modules.Inventories.IntegrationEvents;
using Modules.Orders.IntegrationEvents;
using Newtonsoft.Json;
using SharedKernel.Domain.Entities.Primitives;
using SharedKernel.Persistence;

namespace Modules.Inventories.Api.MessageConsumers;

public sealed class ReserveProductConsumer : IConsumer<ReserveProduct>
{
    private readonly IMediator _mediator;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ILogger<ReserveProductConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public ReserveProductConsumer(
        IMediator mediator,
        IInventoryRepository inventoryRepository,
        ILogger<ReserveProductConsumer> logger,
        IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _inventoryRepository = inventoryRepository;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ReserveProduct> context)
    {
        var inboxMessage = await _inventoryRepository.GetInboxMessageByIdAsync(context.Message.OrderId);
        
        if (inboxMessage is not null && inboxMessage.ProcessedOn is not null)
        {
            _logger.LogInformation($"Order with id {context.Message.OrderId} has already been processed.");
            return;
        }

        var reserveProductResult = await _mediator.Send(new ReserveProductCommand(
            context.Message.OrderId,
            context.Message.ProductId,
            context.Message.QuantityBought));

        if (!reserveProductResult.IsSuccess)
        {
            await _publishEndpoint.Publish(
                new ProductReservedFailedIntegrationEvent(context.Message.OrderId));
            return;
        }

        await _publishEndpoint.Publish(
            new ProductReservedIntegrationEvent(context.Message.OrderId));

        if (inboxMessage is not null)
        {
            inboxMessage.ProcessedOn = DateTime.Now;
        }
        else
        {
            inboxMessage = new InboxMessage(
                           context.Message.OrderId,
                           context.Message.GetType().Name,
                           JsonConvert.SerializeObject(
                                context.Message,
                                new JsonSerializerSettings
                                {
                                    TypeNameHandling = TypeNameHandling.All
                                }),
                           DateTime.Now,
                           DateTime.Now,
                           null);
        }

        await _inventoryRepository.AddOrUpdateInboxMessageAsync(inboxMessage);
    }
}
