using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Models;

namespace Infrastructure;

public static class InfrastructureRegistrationServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        //services.AddMassTransit((busConfigurator) =>
        //{
        //    busConfigurator.SetKebabCaseEndpointNameFormatter();
            
        //    busConfigurator.AddConsumer()

        //    busConfigurator.UsingRabbitMq((context, configurator) =>
        //    {
        //        configurator.Host(configuration["MessageBroker:Host"], h =>
        //        {
        //            h.Username(configuration["MessageBroker:Username"]!);
        //            h.Password(configuration["MessageBroker:Password"]!);
        //        });

        //        configurator.ConfigureEndpoints(context);
        //    });
        //});
        //return services;
    }
}
