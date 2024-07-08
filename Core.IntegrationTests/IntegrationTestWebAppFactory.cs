using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Persistence;
using Modules.Inventories.Persistence;
using Modules.Orders.Persistence;
using Testcontainers.MsSql;
using Xunit;

namespace Core.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime, IDisposable
{
    private readonly MsSqlContainer _orderDbContainer = BuildSqlContainer();

    private readonly MsSqlContainer _inventoryDbContainer = BuildSqlContainer();

    private readonly MsSqlContainer _customerDbContainer = BuildSqlContainer();

    public Task InitializeAsync()
    {
        return Task.WhenAll(
            _orderDbContainer.StartAsync(),
            _inventoryDbContainer.StartAsync(),
            _customerDbContainer.StartAsync());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            RegisterDbContext<OrderDbContext>(services, _orderDbContainer);
            RegisterDbContext<InventoryDbContext>(services, _inventoryDbContainer);
            RegisterDbContext<CustomerDbContext>(services, _customerDbContainer);
        });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.WhenAll(
            _orderDbContainer.StopAsync(),
            _inventoryDbContainer.StopAsync(),
            _customerDbContainer.StopAsync());
    }

    private static MsSqlContainer BuildSqlContainer()
    {
        return new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .Build();
    }

    private void RegisterDbContext<TContext>(IServiceCollection services, MsSqlContainer container) where TContext : DbContext
    {
        var existingDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TContext>));
        if (existingDbContext != null)
        {
            services.Remove(existingDbContext);
        }

        services.AddDbContext<TContext>(options => options.UseSqlServer(container.GetConnectionString()));
    }
}
