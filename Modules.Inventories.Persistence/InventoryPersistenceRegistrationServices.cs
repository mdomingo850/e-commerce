using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Inventories.Application.Contracts;
using Modules.Inventories.Persistence.Reposiories;
using Persistence.Interceptors;

namespace Modules.Inventories.Persistence;

public static class InventoryPersistenceRegistrationServices
{
    public static IServiceCollection AddInventoryRepositoryServices(this IServiceCollection services)
    {
        services.AddDbContext<InventoryDbContext>(
            (sp, optionsBuilder) =>
            {
                var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

                optionsBuilder.UseSqlServer(@"Server=(localdb)\ECommerce;Database=Inventory;Integrated Security=true;")
                    .AddInterceptors(interceptor);
            });
        services.AddScoped<IInventoryRepository, SqlInventoryRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
