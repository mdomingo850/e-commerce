using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;
using Persistence.Repositories.BlankRepositories;
using Persistence.Repositories.SQLRepositories;

namespace Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<SharedDbContext>(
            (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                optionsBuilder.UseSqlServer(@"Server=(localdb)\ECommerce;Database=SingleShared;Integrated Security=true;")
                    //.AddInterceptors(interceptor)
                    ;
            });
        services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
        services.AddScoped<IInventoryRepository, SqlInventoryRepository>();
        services.AddScoped<IOrderRepository, SqlOrderRespository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
