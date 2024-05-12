using Microsoft.Extensions.DependencyInjection;

namespace Modules.Notifications.Application;

public static class NotificationApplicationRegistrationServices
{
    public static IServiceCollection AddNotificationApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<INotificationHandler<OrderCreatedEvent>, SendOrderConfirmationEventHandler>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(NotificationApplicationRegistrationServices).Assembly);

            //config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        return services;
    }
}
