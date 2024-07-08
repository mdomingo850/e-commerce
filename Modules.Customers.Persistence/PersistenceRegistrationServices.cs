using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Persistence.Repositories;

namespace Modules.Customers.Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddCustomerRepositoryServices(this IServiceCollection services)
    {
        var sqlConnectionString = @"Data Source=customersdb;Initial Catalog=Customer;User ID =SA;Password=Password123;TrustServerCertificate=true";
        services.AddDbContext<CustomerDbContext>(
            (sp, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(
                    sqlConnectionString,
                    sqlServerOptions => sqlServerOptions.CommandTimeout(1));
            });
        services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
        services.AddHealthChecks().AddSqlServer(sqlConnectionString, name: "sqlCustomerDB");
        return services;
    }
}
