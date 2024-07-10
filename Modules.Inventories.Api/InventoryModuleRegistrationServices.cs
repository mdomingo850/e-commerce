using Application;
using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Persistence;
using Modules.Inventories.Infrastructure;

namespace Modules.Inventories.Api;

public static class InventoryModuleRegistrationServices
{
    public static IServiceCollection AddInventoryModuleServices(this IServiceCollection services)
    {
        services
            .AddInventoryRepositoryServices()
            .AddInventoryApplicationServices()
            .AddInventoryInfrastructureServices();

        services.AddScoped<IInventoriesApi, InventoriesApi>();
        return services;
    }
}
