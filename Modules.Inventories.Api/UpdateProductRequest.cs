namespace Modules.Inventories.Api;

public record UpdateProductRequest(Guid Id, int QuantityReservedAmount)
{
}
