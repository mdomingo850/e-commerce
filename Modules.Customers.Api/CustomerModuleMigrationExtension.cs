using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Persistence;

namespace Modules.Customers.Api;

public static class CustomerModuleMigrationExtension
{
    public static void ApplyCustomerMigrations(IServiceScope serviceScope)
    {
        using CustomerDbContext context = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();

        context.Database.Migrate();
    }
}
