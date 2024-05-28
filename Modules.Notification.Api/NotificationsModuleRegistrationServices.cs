using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Modules.Notifications.Application;

namespace Modules.Notifications.Api;

public static class NotificationsModuleRegistrationServices
{
    public static IServiceCollection AddNotificationsModuleServices(this IServiceCollection services)
    {
        services
            .AddNotificationApplicationServices()
            .AddNotificationInfrastructureServices();

        return services;
    }

}
