using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationRegistrationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<INotificationHandler<OrderCreatedEvent>, SendOrderConfirmationEventHandler>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(ApplicationRegistrationServices).Assembly);

            //config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        return services;
    }
}
