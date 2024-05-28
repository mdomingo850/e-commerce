using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application.Services;

namespace Application;

public static class CustomerApplicationRegistrationServices
{
    public static IServiceCollection AddCustomerApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CustomerService>();

        return services;
    }
}
