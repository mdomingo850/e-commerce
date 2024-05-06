using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Modules.Notifications.Application.Contracts;

namespace Infrastructure;

public static class InfrastructureRegistrationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, BlankNotificationService>();
        
        return services;
    }
}
