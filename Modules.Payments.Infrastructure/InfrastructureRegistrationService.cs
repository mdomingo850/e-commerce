using Microsoft.Extensions.DependencyInjection;
using Modules.Payments.Application.Contracts;

namespace Modules.Payments.Infrastructure;

public static class PaymentInfrastructureRegistrationService
{
    public static IServiceCollection AddPaymentInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
