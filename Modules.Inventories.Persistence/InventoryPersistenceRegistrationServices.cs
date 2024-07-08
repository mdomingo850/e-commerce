using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Persistence.Reposiories;

namespace Modules.Inventories.Persistence;

public static class InventoryPersistenceRegistrationServices
{
    public static IServiceCollection AddInventoryRepositoryServices(this IServiceCollection services)
    {
        var sqlConnectionString = @"Data Source=inventoriesdb;Initial Catalog=Inventory;User ID =SA;Password=Password123;TrustServerCertificate=true";
        services.AddDbContext<InventoryDbContext>(
            (sp, optionsBuilder) =>
            {

                optionsBuilder.UseSqlServer(sqlConnectionString);
            });
        services.AddScoped<IInventoryRepository, SqlInventoryRepository>();
        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHealthChecks().AddSqlServer(sqlConnectionString, name: "sqlInventoryDB");
        return services;
    }
}
