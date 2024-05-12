using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class PaymentApplicationRegistrationServices
{
    public static IServiceCollection AddPaymentApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(typeof(PaymentApplicationRegistrationServices).Assembly);
        });

        return services;
    }
}
