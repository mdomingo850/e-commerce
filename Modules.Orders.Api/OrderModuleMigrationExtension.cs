using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Persistence;

namespace Modules.Customers.Api;

public static class OrderModuleMigrationExtension
{
    public static void ApplyOrderMigrations(IServiceScope serviceScope)
    {
        using OrderDbContext context = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();

        context.Database.Migrate();
    }
}
