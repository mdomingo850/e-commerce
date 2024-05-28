using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class InventoryApplicationRegistrationServices
{
    public static IServiceCollection AddInventoryApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(InventoryApplicationRegistrationServices).Assembly);
        });

        return services;
    }
}
