using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Api;
using Modules.Customers.Application;

namespace Infrastructure;

public static class CustomersInfrastructureRegistrationService
{
    public static IServiceCollection AddCustomerInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersApi, CustomersApi>();
        
        return services;
    }
}
