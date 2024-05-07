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
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<OrderDbContext>(
            (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                optionsBuilder.UseSqlServer(@"Server=(localdb)\ECommerce;Database=Order;Integrated Security=true;")
                    .AddInterceptors(interceptor);
            });
        services.AddScoped<IOrderRepository, SqlOrderRespository>();
        return services;
    }
}
