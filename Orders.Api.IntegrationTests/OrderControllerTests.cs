using Core.IntegrationTests;
using Modules.Orders.Api.RequestPayload;
using Modules.Orders.Application.Features.CreateOrder;
using Modules.Orders.Domain.Entities;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Orders.Api.IntegrationTests;

public class OrderControllerTests : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public OrderControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Create_WhenValidationPasses_ShouldReturn200Ok(int quantityBought)
    {
        //Arrange
        var client = _factory.CreateClient();
        var customerId = Guid.Parse("AC8572BA-8742-43BE-AC63-FD69654A7188");
        var orderItem = new HashSet<OrderItemModel>() { new(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), quantityBought) };
        var payload = new CreatePayload(customerId, orderItem);

        var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync("/order", jsonContent);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "5F341EC0-38F2-4A3E-84D7-1EB51885A95D")]
    [InlineData("AC8572BA-8742-43BE-AC63-FD69654A7188", "00000000-0000-0000-0000-000000000000")]
    public async Task Create_WhenCustomerOrProductNotFound_ShouldReturnNotFound(string customerGuid, string productGuid)
    {
        //Arrange
        var client = _factory.CreateClient();
        var customerId = Guid.Parse(customerGuid);
        var orderItem = new HashSet<OrderItemModel>() { new(Guid.Parse(productGuid), 1) };
        var payload = new CreatePayload(customerId, orderItem);

        var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync("/order", jsonContent);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData(10000)]
    [InlineData(20000)]
    public async Task Create_WhenItemOutOfStock_ShouldReturnConflict(int quantityBought)
    {
        //Arrange
        var client = _factory.CreateClient();
        var customerId = Guid.Parse("AC8572BA-8742-43BE-AC63-FD69654A7188");
        var orderItem = new HashSet<OrderItemModel>() { new(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), quantityBought) };
        var payload = new CreatePayload(customerId, orderItem);

        var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        //Act
        var response = await client.PostAsync("/order", jsonContent);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}