using Application.Contracts;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureRegistrationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, BlankPaymentService>();
        services.AddScoped<INotificationService, BlankNotificationService>();

        return services;
    }
}
