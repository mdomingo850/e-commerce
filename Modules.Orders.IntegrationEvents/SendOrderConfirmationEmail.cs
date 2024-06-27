namespace Modules.Orders.IntegrationEvents;

public sealed record SendOrderConfirmationEmail(
    Guid OrderId
    //,
    //string CustomerEmail,
    //string CustomerName,
    //string ProductName,
    //string ProductQuantity
    )
{
}
