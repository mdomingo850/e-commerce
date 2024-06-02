using Modules.Customers.Api;

namespace E_Commerce;

public static class CustomersMigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        CustomerModuleMigrationExtension.ApplyCustomerMigrations(scope);
        InventoryModuleMigrationExtension.ApplyInventoryMigrations(scope);
        OrderModuleMigrationExtension.ApplyOrderMigrations(scope);
    }
}
