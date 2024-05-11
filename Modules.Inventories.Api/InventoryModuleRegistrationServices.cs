using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Persistence;

namespace Modules.Inventories.Api;

public static class InventoryModuleRegistrationServices
{
    public static IServiceCollection AddInventoryModuleServices(this IServiceCollection services)
    {
        services.AddInventoryRepositoryServices();
        services.AddScoped<IInventoriesApi, InventoriesApi>();
        return services;
    }
}
