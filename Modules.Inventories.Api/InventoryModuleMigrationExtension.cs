using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Persistence;

namespace Modules.Customers.Api;

public static class InventoryModuleMigrationExtension
{
    public static void ApplyInventoryMigrations(IServiceScope serviceScope)
    {
        using InventoryDbContext context = serviceScope.ServiceProvider.GetRequiredService<InventoryDbContext>();

        context.Database.Migrate();
    }
}
