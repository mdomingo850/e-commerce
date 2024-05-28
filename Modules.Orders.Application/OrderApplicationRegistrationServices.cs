using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Contracts;
using Modules.Orders.Application.Services;

namespace Application;

public static class OrderApplicationRegistrationServices
{
    public static IServiceCollection AddOrderApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<INotificationHandler<OrderCreatedEvent>, SendOrderConfirmationEventHandler>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(OrderApplicationRegistrationServices).Assembly);

            //config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
