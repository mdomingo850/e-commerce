using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Persistence;
using Xunit;

namespace Core.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly OrderDbContext OrdersDbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        OrdersDbContext = _scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    }
}
