using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Persistence.Interceptors;
using Modules.Orders.Persistence.Repositories;

namespace Modules.Orders.Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddOrderRepositoryServices(this IServiceCollection services)
    {
        var sqlConnectionString = @"Data Source=ordersdb;Initial Catalog=Order;User ID =SA;Password=Password123;TrustServerCertificate=true";
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<OrderDbContext>(
            (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                optionsBuilder.UseSqlServer(sqlConnectionString)
                    .AddInterceptors(interceptor);
            });
        services.AddScoped<IOrderRepository, SqlOrderRespository>();
        services.AddHealthChecks().AddSqlServer(sqlConnectionString, name: "sqlOrderDB");
        return services;
    }
}
