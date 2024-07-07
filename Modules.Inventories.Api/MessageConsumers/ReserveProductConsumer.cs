using MassTransit;
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

    public ReserveProductConsumer(
        IMediator mediator,
        IInventoryRepository inventoryRepository,
        ILogger<ReserveProductConsumer> logger)
    {
        _mediator = mediator;
        _inventoryRepository = inventoryRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReserveProduct> context)
    {
        if (await _inventoryRepository.HasInboxMessage(context.Message.OrderId))
        {
            _logger.LogInformation($"Order with id {context.Message.OrderId} already has a message in the inbox");
            return;
        }

        var domainEvent = new ReserveProductDomainEvent(
                                context.Message.OrderId,
                                context.Message.ProductId,
                                context.Message.QuantityBought);

        var inboxMessage = new InboxMessage(
                       domainEvent.OrderId, 
                       domainEvent.GetType().Name,
                       JsonConvert.SerializeObject(
                            domainEvent,
                            new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.All
                            }),
                       DateTime.Now,
                       null,
                       null);

        await _inventoryRepository.AddInboxMessage(inboxMessage);
    }
}
