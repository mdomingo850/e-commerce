using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Persistence.Repositories;
using Persistence.Interceptors;

namespace Modules.Customers.Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddCustomerRepositoryServices(this IServiceCollection services)
    {
        services.AddDbContext<CustomerDbContext>(
            (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                optionsBuilder.UseSqlServer(@"Server=(localdb)\ECommerce;Database=Customer;Integrated Security=true;")
                    .AddInterceptors(interceptor);
            });
        services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
        return services;
    }
}
