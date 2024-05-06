using Application;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Persistence;

namespace Modules.Customers.Api;

public static class CustomerModuleRegistrationServices
{
    public static IServiceCollection AddCustomerModuleServices(this IServiceCollection services)
    {
        services.AddCustomerRepositoryServices().AddCustomerApplicationServices();

        services.AddScoped<ICustomersApi, CustomersApi>();
        return services;
    }
}
