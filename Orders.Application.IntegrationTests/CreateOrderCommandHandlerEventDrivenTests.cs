using Core.IntegrationTests;
using Modules.Orders.Application.Features.CreateOrder;

namespace Orders.Application.IntegrationTests;

public class CreateOrderCommandHandlerEventDrivenTests : BaseIntegrationTest
{
    public CreateOrderCommandHandlerEventDrivenTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Handle_WhenValidationPasses_ShouldCreateOrderRecordToDatabase(int quantityBought)
    {
        //Arrange
        var clarkKentCustomerId = Guid.Parse("AC8572BA-8742-43BE-AC63-FD69654A7188");
        var orderItems = new HashSet<OrderItemModel>() { new(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), quantityBought) };
        var command = new CreateOrderCommand(clarkKentCustomerId, orderItems);

        //Act
        await Sender.Send(command).ConfigureAwait(false);

        //Assert
        var order = OrdersDbContext.Orders.FirstOrDefault();
        Assert.True(order.OrderItems.First().Quanitity == quantityBought);
        Assert.Single(OrdersDbContext.Orders);

        //Cleanup
        OrdersDbContext.Orders.Remove(order);
        OrdersDbContext.SaveChanges();
    }
}
