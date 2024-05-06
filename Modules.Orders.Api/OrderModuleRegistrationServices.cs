using Application;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Persistence;

namespace Modules.Customers.Api;

public static class OrderModuleRegistrationServices
{
    public static IServiceCollection AddOrderModuleServices(this IServiceCollection services)
    {
        services
            .AddOrderRepositoryServices()
            .AddOrderApplicationServices();
        return services;
    }
}
