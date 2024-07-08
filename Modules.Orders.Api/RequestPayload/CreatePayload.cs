using Modules.Orders.Application.Features.CreateOrder;

namespace Modules.Orders.Api.RequestPayload;

public record CreatePayload(Guid CustomerId, HashSet<OrderItemModel> OrderItems);