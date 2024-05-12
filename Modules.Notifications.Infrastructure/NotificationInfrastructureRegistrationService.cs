using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Modules.Notifications.Application.Contracts;
using Modules.Notifications.Application.Features;
using MassTransit;

namespace Infrastructure;

public static class NotificationInfrastructureRegistrationService
{
    public static IServiceCollection AddNotificationInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, BlankNotificationService>();


        return services;
    }
}
