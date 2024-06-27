namespace Modules.Orders.Application.Features.CreateOrder;

public record OrderItemModel(Guid ProductId, int Quantity);