using Application;
using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Persistence;

namespace Modules.Inventories.Api;

public static class InventoryModuleRegistrationServices
{
    public static IServiceCollection AddInventoryModuleServices(this IServiceCollection services)
    {
        services
            .AddInventoryRepositoryServices()
            .AddInventoryApplicationServices();
        services.AddScoped<IInventoriesApi, InventoriesApi>();
        return services;
    }
}
