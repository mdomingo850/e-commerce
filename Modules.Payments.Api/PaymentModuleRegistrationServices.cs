using Microsoft.Extensions.DependencyInjection;
using Modules.Payments.Infrastructure;

namespace Modules.Payments.Api;

public static class PaymentModuleRegistrationServices
{
    public static IServiceCollection AddPaymentModuleServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentsApi, PaymentsApi>();
        services.AddPaymentInfrastructureServices();
        return services;
    }
}
